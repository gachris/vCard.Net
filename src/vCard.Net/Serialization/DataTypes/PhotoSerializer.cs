using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for <see cref="Photo"/> objects.
/// </summary>
public class PhotoSerializer : EncodableDataTypeSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PhotoSerializer"/> class.
    /// </summary>
    public PhotoSerializer()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PhotoSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public PhotoSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(Photo);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        return obj is not Photo photo ? null : photo.Value;
    }

    /// <summary>
    /// Deserializes the specified value into a <see cref="Photo"/> object.
    /// </summary>
    /// <param name="value">The string value to deserialize.</param>
    /// <returns>The deserialized <see cref="Photo"/> object.</returns>
    public Photo Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not Photo photo)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(photo, value);

        if (value is null)
        {
            return null;
        }

        photo.Value = value;

        return photo;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}
