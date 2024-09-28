using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for enum types, providing methods to serialize and deserialize enum values.
/// </summary>
public class EnumSerializer : EncodableDataTypeSerializer
{
    private readonly Type _mEnumType;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnumSerializer"/> class with the specified enum type.
    /// </summary>
    /// <param name="enumType">The type of the enum to be serialized.</param>
    public EnumSerializer(Type enumType) => _mEnumType = enumType;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnumSerializer"/> class with the specified enum type and serialization context.
    /// </summary>
    /// <param name="enumType">The type of the enum to be serialized.</param>
    /// <param name="ctx">The serialization context.</param>
    public EnumSerializer(Type enumType, SerializationContext ctx) : base(ctx) => _mEnumType = enumType;

    /// <inheritdoc/>
    public override Type TargetType => _mEnumType;

    /// <inheritdoc/>
    public override string SerializeToString(object enumValue)
    {
        try
        {
            if (SerializationContext.Peek() is IVCardObject obj)
            {
                // Encode the value as needed.
                var dt = new EncodableDataType
                {
                    AssociatedObject = obj
                };
                return Encode(dt, enumValue.ToString());
            }
            return enumValue.ToString();
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

            // Remove "-" characters while parsing Enum values.
            return Enum.Parse(_mEnumType, value.Replace("-", ""), true);
        }
        catch { }

        return value;
    }
}