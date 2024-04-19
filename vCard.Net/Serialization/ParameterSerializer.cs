using System;
using System.IO;
using System.Text;

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
        if (!(obj is vCardParameter p))
        {
            return null;
        }

        var builder = new StringBuilder();
        builder.Append(p.Name + "=");

        // "Section 3.2:  Property parameter values MUST NOT contain the DQUOTE character."
        // Therefore, let's strip any double quotes from the value.
        var values = string.Join(",", p.Values).Replace("\"", string.Empty);

        // TODO: Should remove the following method?

        // Surround the parameter value with double quotes, if the value
        // contains any problematic characters.
        //if (values.IndexOfAny([';', ':', ',']) >= 0)
        //{
        //    values = "\"" + values + "\"";
        //}
        builder.Append(values);
        return builder.ToString();
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => null;
}