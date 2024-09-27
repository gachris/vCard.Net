using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Abstract base class for encodable data type serializers, providing methods for encoding and decoding data.
/// </summary>
public abstract class EncodableDataTypeSerializer : DataTypeSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EncodableDataTypeSerializer"/> class.
    /// </summary>
    protected EncodableDataTypeSerializer() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="EncodableDataTypeSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    protected EncodableDataTypeSerializer(SerializationContext ctx) : base(ctx) { }

    /// <summary>
    /// Encodes the specified value using the encoding specified in the <paramref name="dt"/> object.
    /// </summary>
    /// <param name="dt">The encodable data type object containing encoding information.</param>
    /// <param name="value">The value to encode.</param>
    /// <returns>The encoded string.</returns>
    protected string Encode(IEncodableDataType dt, string value)
    {
        if (value == null)
        {
            return null;
        }

        if (dt?.Encoding == null)
        {
            return value;
        }

        // Return the value in the current encoding
        var encodingStack = GetService<EncodingStack>();
        return Encode(dt, encodingStack.Current.GetBytes(value));
    }

    /// <summary>
    /// Encodes the specified data using the encoding specified in the <paramref name="dt"/> object.
    /// </summary>
    /// <param name="dt">The encodable data type object containing encoding information.</param>
    /// <param name="data">The data to encode.</param>
    /// <returns>The encoded string.</returns>
    protected string Encode(IEncodableDataType dt, byte[] data)
    {
        if (data == null)
        {
            return null;
        }

        if (dt?.Encoding == null)
        {
            // Default to the current encoding
            var encodingStack = GetService<EncodingStack>();
            return encodingStack.Current.GetString(data);
        }

        var encodingProvider = GetService<IEncodingProvider>();
        return encodingProvider?.Encode(dt.Encoding, data);
    }

    /// <summary>
    /// Decodes the specified value using the encoding specified in the <paramref name="dt"/> object.
    /// </summary>
    /// <param name="dt">The encodable data type object containing encoding information.</param>
    /// <param name="value">The value to decode.</param>
    /// <returns>The decoded string.</returns>
    protected string Decode(IEncodableDataType dt, string value)
    {
        if (dt?.Encoding == null)
        {
            return value;
        }

        var data = DecodeData(dt, value);
        if (data == null)
        {
            return null;
        }

        // Default to the current encoding
        var encodingStack = GetService<EncodingStack>();
        return encodingStack.Current.GetString(data);
    }

    /// <summary>
    /// Decodes the specified value data using the encoding specified in the <paramref name="dt"/> object.
    /// </summary>
    /// <param name="dt">The encodable data type object containing encoding information.</param>
    /// <param name="value">The value data to decode.</param>
    /// <returns>The decoded byte array.</returns>
    protected byte[] DecodeData(IEncodableDataType dt, string value)
    {
        if (value == null)
        {
            return null;
        }

        if (dt?.Encoding == null)
        {
            // Default to the current encoding
            var encodingStack = GetService<EncodingStack>();
            return encodingStack.Current.GetBytes(value);
        }

        var encodingProvider = GetService<IEncodingProvider>();
        return encodingProvider?.DecodeData(dt.Encoding, value);
    }
}