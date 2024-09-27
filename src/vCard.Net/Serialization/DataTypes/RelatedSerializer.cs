using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for <see cref="Related"/> objects.
/// </summary>
public class RelatedSerializer : EncodableDataTypeSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RelatedSerializer"/> class.
    /// </summary>
    public RelatedSerializer() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RelatedSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public RelatedSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(Related);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        return obj is not Related related ? null : Encode(related, related.Value);
    }

    /// <summary>
    /// Deserializes the specified value into a <see cref="Related"/> object.
    /// </summary>
    /// <param name="value">The string value to deserialize.</param>
    /// <returns>The deserialized <see cref="Related"/> object.</returns>
    public Related Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not Related related)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(related, value);

        if (value is null)
        {
            return null;
        }

        related.Value = value;

        return related;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}
