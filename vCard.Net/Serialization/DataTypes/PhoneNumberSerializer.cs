using System;
using System.IO;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for <see cref="PhoneNumber"/> objects.
/// </summary>
public class PhoneNumberSerializer : EncodableDataTypeSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PhoneNumberSerializer"/> class.
    /// </summary>
    public PhoneNumberSerializer()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PhoneNumberSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public PhoneNumberSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(PhoneNumber);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        return obj is not PhoneNumber phoneNumber ? null : Encode(phoneNumber, phoneNumber.Value);
    }

    /// <summary>
    /// Deserializes the specified value into a <see cref="PhoneNumber"/> object.
    /// </summary>
    /// <param name="value">The string value to deserialize.</param>
    /// <returns>The deserialized <see cref="PhoneNumber"/> object.</returns>
    public PhoneNumber Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not PhoneNumber phoneNumber)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(phoneNumber, value);

        if (value is null)
        {
            return null;
        }

        phoneNumber.Value = value;

        return phoneNumber;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}