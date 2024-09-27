using System;
using System.IO;
using System.Text;
using vCard.Net.CardComponents;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization;

/// <summary>
/// Serializer for vCard parameter objects.
/// </summary>
public class ParameterSerializer : SerializerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterSerializer"/> class.
    /// </summary>
    public ParameterSerializer() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public ParameterSerializer(SerializationContext ctx) : base(ctx) { }

    /// <inheritdoc/>
    public override Type TargetType => typeof(vCardParameter);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        if (obj is not vCardParameter parameter)
        {
            return null;
        }

        var version = vCardVersion.vCard2_1;

        if (SerializationContext.Peek() is IvCardProperty property && property.Parent is IvCardComponent component)
        {
            version = component.Version;
        }

        var builder = new StringBuilder();
        builder.Append(parameter.Name + "=");

        var separator = version is vCardVersion.vCard2_1 ? ";" : ",";

        // "Section 3.2:  Property parameter values MUST NOT contain the DQUOTE character."
        // Therefore, let's strip any double quotes from the value.
        var values = string.Join(separator, parameter.Values).Replace("\"", string.Empty);

        builder.Append(values);
        return builder.ToString();
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => null;
}