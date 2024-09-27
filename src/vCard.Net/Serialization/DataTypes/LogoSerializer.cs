using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for <see cref="Logo"/> objects.
/// </summary>
public class LogoSerializer : EncodableDataTypeSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LogoSerializer"/> class.
    /// </summary>
    public LogoSerializer()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LogoSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public LogoSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(Logo);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        return obj is not Logo logo ? null : Encode(logo, logo.Value);
    }

    /// <summary>
    /// Deserializes the specified value into a <see cref="Logo"/> object.
    /// </summary>
    /// <param name="value">The string value to deserialize.</param>
    /// <returns>The deserialized <see cref="Logo"/> object.</returns>
    public Logo Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not Logo logo)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(logo, value);

        if (value is null)
        {
            return null;
        }

        logo.Value = value;

        return logo;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}
