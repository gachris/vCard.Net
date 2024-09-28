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
    public override Type TargetType => typeof(VCardParameter);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        if (obj is not VCardParameter parameter)
        {
            return null;
        }

        var version = VCardVersion.vCard2_1;

        if (SerializationContext.Peek() is IVCardProperty property && property.Parent is IVCardComponent component)
        {
            version = component.Version;
        }

        var builder = new StringBuilder();

        if (!string.IsNullOrEmpty(parameter.Name))
        {
            builder.Append(parameter.Name + "=");
        }

        var separator = version is VCardVersion.vCard2_1 ? ";" : ",";

        // "Section 3.2:  Property parameter values MUST NOT contain the DQUOTE character."
        // Therefore, let's strip any double quotes from the value.
        var values = string.Join(separator, parameter.Values).Replace("\"", string.Empty);

        builder.Append(values);
        return builder.ToString();
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => null;
}