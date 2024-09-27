using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for <see cref="IMPP"/> objects, providing methods to serialize and deserialize instant messaging and presence protocol (IMPP) information.
/// </summary>
public class IMPPSerializer : EncodableDataTypeSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IMPPSerializer"/> class.
    /// </summary>
    public IMPPSerializer() : base() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="IMPPSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public IMPPSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(IMPP);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        return obj is not IMPP impp ? null : Encode(impp, impp.Value);
    }

    /// <summary>
    /// Deserializes the string value into an <see cref="IMPP"/> object.
    /// </summary>
    /// <param name="value">The string value to deserialize.</param>
    /// <returns>The deserialized <see cref="IMPP"/> object.</returns>
    public IMPP Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not IMPP impp)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(impp, value);

        if (value is null)
        {
            return null;
        }

        impp.Value = value;

        return impp;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}