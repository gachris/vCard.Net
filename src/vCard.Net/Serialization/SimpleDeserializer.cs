﻿using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using vCard.Net.CardComponents;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization;

/// <summary>
/// Responsible for deserializing vCard components from a TextReader.
/// </summary>
public class SimpleDeserializer
{
    internal SimpleDeserializer(
        DataTypeMapper dataTypeMapper,
        ISerializerFactory serializerFactory,
        VCardComponentFactory componentFactory)
    {
        _dataTypeMapper = dataTypeMapper;
        _serializerFactory = serializerFactory;
        _componentFactory = componentFactory;
    }

    /// <summary>
    /// Default instance of <see cref="SimpleDeserializer"/> initialized with default dependencies.
    /// </summary>
    public static readonly SimpleDeserializer Default = new SimpleDeserializer(
        new DataTypeMapper(),
        new SerializerFactory(),
        new VCardComponentFactory());

    private const string _nameGroup = "name";
    private const string _valueGroup = "value";
    private const string _paramNameGroup = "paramName";
    private const string _paramValueGroup = "paramValue";

    private static readonly Regex _contentLineRegex = new Regex(BuildContentLineRegex(), RegexOptions.Compiled);

    private readonly DataTypeMapper _dataTypeMapper;
    private readonly ISerializerFactory _serializerFactory;
    private readonly VCardComponentFactory _componentFactory;

    private static string BuildContentLineRegex()
    {
        // name          = iana-token / x-name
        // iana-token    = 1*(ALPHA / DIGIT / "-")
        // x-name        = "X-" [vendorid "-"] 1*(ALPHA / DIGIT / "-")
        // vendorid      = 3*(ALPHA / DIGIT)
        // Add underscore to match behavior of bug 2033495
        const string identifier = "[-A-Za-z0-9_.]+";

        // param-value   = paramtext / quoted-string
        // paramtext     = *SAFE-CHAR
        // quoted-string = DQUOTE *QSAFE-CHAR DQUOTE
        // QSAFE-CHAR    = WSP / %x21 / %x23-7E / NON-US-ASCII
        // ; Any character except CONTROL and DQUOTE
        // SAFE-CHAR     = WSP / %x21 / %x23-2B / %x2D-39 / %x3C-7E
        //               / NON-US-ASCII
        // ; Any character except CONTROL, DQUOTE, ";", ":", ","
        var paramValue = $"((?<{_paramValueGroup}>[^\\x00-\\x08\\x0A-\\x1F\\x7F\";:,]*)|\"(?<{_paramValueGroup}>[^\\x00-\\x08\\x0A-\\x1F\\x7F\"]*)\")";

        // param         = param-name "=" param-value *("," param-value)
        // param-name    = iana-token / x-name
        var paramName = $"(?<{_paramNameGroup}>{identifier})";
        var param = $"{paramName}={paramValue}(,{paramValue})*";
        //var param = $"{paramName}(=)?{paramValue}(,{paramValue})*";

        // contentline   = name *(";" param ) ":" value CRLF
        var name = $"(?<{_nameGroup}>{identifier})";
        // value         = *VALUE-CHAR
        var value = $"(?<{_valueGroup}>[^\\x00-\\x08\\x0E-\\x1F\\x7F]*)";
        var contentLine = $"^{name}(;{param})*:{value}$";
        return contentLine;
    }

    /// <summary>
    /// Deserializes vCard components from the provided TextReader.
    /// </summary>
    /// <param name="reader">The TextReader containing the vCard data.</param>
    /// <returns>An IEnumerable of IvCardComponent representing the deserialized vCard components.</returns>
    public IEnumerable<IVCardComponent> Deserialize(TextReader reader)
    {
        var context = new SerializationContext();
        var stack = new Stack<IVCardComponent>();
        var current = default(IVCardComponent);
        foreach (var contentLineString in GetContentLines(reader))
        {
            var contentLine = ParseContentLine(context, contentLineString);
            if (string.Equals(contentLine.Name, "BEGIN", StringComparison.OrdinalIgnoreCase))
            {
                stack.Push(current);
                current = _componentFactory.Build((string)contentLine.Value);
                SerializationUtil.OnDeserializing(current);
            }
            else if (string.Equals(contentLine.Name, "AGENT", StringComparison.OrdinalIgnoreCase))
            {
                stack.Push(current);
                current = _componentFactory.Build("AGENT");
                SerializationUtil.OnDeserializing(current);
            }
            else
            {
                if (current == null)
                {
                    throw new SerializationException($"Expected 'BEGIN', found '{contentLine.Name}'");
                }
                if (string.Equals(contentLine.Name, "END", StringComparison.OrdinalIgnoreCase))
                {
                    if (!string.Equals((string)contentLine.Value, current.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        throw new SerializationException($"Expected 'END:{current.Name}', found 'END:{contentLine.Value}'");
                    }
                    SerializationUtil.OnDeserialized(current);
                    var finished = current;
                    current = stack.Pop();
                    if (current == null)
                    {
                        yield return finished;
                    }
                    else
                    {
                        current.Children.Add(finished);
                    }
                }
                else
                {
                    if (string.Equals(contentLine.Name, "LABEL", StringComparison.OrdinalIgnoreCase))
                    {
                        var address = current.Properties.GetMany<Address>("ADR").LastOrDefault();
                        if (address is not null)
                        {
                            address.Label = (Label)contentLine.Value;
                        }
                    }
                    else
                    {
                        current.Properties.Add(contentLine);
                    }
                }
            }
        }
        if (current != null)
        {
            throw new SerializationException($"Unclosed component {current.Name}");
        }
    }

    private VCardProperty ParseContentLine(SerializationContext context, string input)
    {
        var pattern = "(?<name>[-A-Za-z0-9_.]+)(?<type>(;[A-Z]+)+):";

        var types = default(string);
        var typeMatch = Regex.Match(input, pattern);
        if (typeMatch.Success)
        {
            types = typeMatch.Groups["type"].Value;
            input = input.Replace(types, $";TYPE={types.Trim(';').Replace(";", ",")}");
        }

        var match = _contentLineRegex.Match(input);
        if (!match.Success)
        {
            throw new SerializationException($"Could not parse line: '{input}'");
        }
        var name = match.Groups[_nameGroup].Value;
        var value = match.Groups[_valueGroup].Value;
        var paramNames = match.Groups[_paramNameGroup].Captures;
        var paramValues = match.Groups[_paramValueGroup].Captures;

        var property = new VCardProperty(name.ToUpperInvariant());
        context.Push(property);
        SetPropertyParameters(property, paramNames, paramValues);
        SetPropertyValue(context, property, value);
        context.Pop();
        return property;
    }

    private static void SetPropertyParameters(VCardProperty property, CaptureCollection paramNames, CaptureCollection paramValues)
    {
        var paramValueIndex = 0;
        for (var paramNameIndex = 0; paramNameIndex < paramNames.Count; paramNameIndex++)
        {
            var paramName = paramNames[paramNameIndex].Value;
            var parameter = new VCardParameter(paramName);
            var nextParamIndex = paramNameIndex + 1 < paramNames.Count ? paramNames[paramNameIndex + 1].Index : int.MaxValue;
            while (paramValueIndex < paramValues.Count && paramValues[paramValueIndex].Index < nextParamIndex)
            {
                var paramValue = paramValues[paramValueIndex].Value;
                parameter.AddValue(paramValue);
                paramValueIndex++;
            }
            property.AddParameter(parameter);
        }
    }

    private void SetPropertyValue(SerializationContext context, VCardProperty property, string value)
    {
        var type = _dataTypeMapper.GetPropertyMapping(property) ?? typeof(string);
        var serializer = (SerializerBase)_serializerFactory.Build(type, context);
        using var valueReader = new StringReader(value);
        var propertyValue = serializer.Deserialize(valueReader);
        if (propertyValue is IEnumerable<string> propertyValues)
        {
            foreach (var singlePropertyValue in propertyValues)
            {
                property.AddValue(singlePropertyValue);
            }
        }
        else
        {
            property.AddValue(propertyValue);
        }
    }

    private static IEnumerable<string> GetContentLines(TextReader reader)
    {
        var currentLine = new StringBuilder();
        while (true)
        {
            var nextLine = reader.ReadLine();
            if (nextLine == null)
            {
                break;
            }

            if (nextLine.Length <= 0)
            {
                continue;
            }

            if (nextLine[0] is ' ' or '\t')
            {
                currentLine.Append(nextLine, 1, nextLine.Length - 1);
            }
            else
            {
                if (currentLine.Length > 0)
                {
                    yield return currentLine.ToString();
                }
                currentLine.Clear();
                currentLine.Append(nextLine);
            }
        }
        if (currentLine.Length > 0)
        {
            yield return currentLine.ToString();
        }
    }
}