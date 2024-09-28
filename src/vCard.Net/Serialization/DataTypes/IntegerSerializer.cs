using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for <see cref="int"/> values, providing methods to serialize and deserialize integer data types.
/// </summary>
public class IntegerSerializer : EncodableDataTypeSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IntegerSerializer"/> class.
    /// </summary>
    public IntegerSerializer() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="IntegerSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public IntegerSerializer(SerializationContext ctx) : base(ctx) { }

    /// <inheritdoc/>
    public override Type TargetType => typeof(int);

    /// <inheritdoc/>
    public override string SerializeToString(object integer)
    {
        try
        {
            var i = Convert.ToInt32(integer);

            if (SerializationContext.Peek() is IVCardObject obj)
            {
                // Encode the value as needed.
                var dt = new EncodableDataType
                {
                    AssociatedObject = obj
                };
                return Encode(dt, i.ToString());
            }
            return i.ToString();
        }
        catch
        {
            return null;
        }
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr)
    {
        var value = tr.ReadToEnd();

        try
        {
            if (SerializationContext.Peek() is IVCardObject obj)
            {
                // Decode the value, if necessary!
                var dt = new EncodableDataType
                {
                    AssociatedObject = obj
                };
                value = Decode(dt, value);
            }

            if (int.TryParse(value, out int i))
            {
                return i;
            }
        }
        catch { }

        return value;
    }
}