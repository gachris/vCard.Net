using System;
using System.IO;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for URI data type, providing serialization and deserialization methods.
/// </summary>
public class UriSerializer : EncodableDataTypeSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UriSerializer"/> class.
    /// </summary>
    public UriSerializer() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="UriSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public UriSerializer(SerializationContext ctx) : base(ctx) { }

    /// <inheritdoc/>
    public override Type TargetType => typeof(string);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        if (obj is not Uri)
        {
            return null;
        }

        var uri = (Uri)obj;

        if (SerializationContext.Peek() is IvCardObject co)
        {
            var dt = new EncodableDataType
            {
                AssociatedObject = co
            };
            return Encode(dt, uri.OriginalString);
        }
        return uri.OriginalString;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr)
    {
        if (tr == null)
        {
            return null;
        }

        var value = tr.ReadToEnd();

        if (SerializationContext.Peek() is IvCardObject co)
        {
            var dt = new EncodableDataType
            {
                AssociatedObject = co
            };
            value = Decode(dt, value);
        }

        try
        {
            var uri = new Uri(value);
            return uri;
        }
        catch { }
        return null;
    }
}