using System;
using System.IO;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializes and deserializes Address objects.
/// </summary>
public class AddressSerializer : EncodableDataTypeSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddressSerializer"/> class.
    /// </summary>
    public AddressSerializer() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AddressSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public AddressSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(Address);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        return obj is not Address address ? null : Encode(address, address.Value);
    }

    /// <summary>
    /// Deserializes an Address object from a string value.
    /// </summary>
    /// <param name="value">The string representation of the Address object.</param>
    /// <returns>The deserialized Address object.</returns>
    public Address Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not Address address)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(address, value);

        if (value is null)
        {
            return null;
        }

        address.Value = value;

        return address;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}
