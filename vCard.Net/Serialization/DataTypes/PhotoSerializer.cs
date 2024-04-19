using System;
using System.IO;
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
        throw new NotImplementedException();
    }

    /// <summary>
    /// Deserializes the specified value into a <see cref="Photo"/> object.
    /// </summary>
    /// <param name="value">The string value to deserialize.</param>
    /// <returns>The deserialized <see cref="Photo"/> object.</returns>
    public Photo Deserialize(string value)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}
