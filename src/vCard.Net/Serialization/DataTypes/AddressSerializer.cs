using System.Text.RegularExpressions;
using vCard.Net.CardComponents;
using vCard.Net.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializes and deserializes Address objects.
/// </summary>
public class AddressSerializer : EncodableDataTypeSerializer
{
    private static readonly Regex _reSplitSemiColon = new Regex("(?:^[;])|(?<=(?:[^\\\\]))[;]");

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
        if (obj is not Address address)
        {
            return null;
        }

        var version = VCardVersion.vCard2_1;
        if (SerializationContext.Peek() is IVCardProperty property && property.Parent is IVCardComponent component)
        {
            version = component.Version;
        }

        var num = 0;
        var array = new string[7];

        if (address.POBox != null && address.POBox.Length > 0)
        {
            num = 1;
            array[0] = version == VCardVersion.vCard2_1 ? address.POBox.RestrictedEscape() : address.POBox.Escape();
        }

        if (address.ExtendedAddress != null && address.ExtendedAddress.Length > 0)
        {
            num = 2;
            array[1] = version == VCardVersion.vCard2_1 ? address.ExtendedAddress.RestrictedEscape() : address.ExtendedAddress.Escape();
        }

        if (address.StreetAddress != null && address.StreetAddress.Length > 0)
        {
            num = 3;
            array[2] = version == VCardVersion.vCard2_1 ? address.StreetAddress.RestrictedEscape() : address.StreetAddress.Escape();
        }

        if (address.Locality != null && address.Locality.Length > 0)
        {
            num = 4;
            array[3] = version == VCardVersion.vCard2_1 ? address.Locality.RestrictedEscape() : address.Locality.Escape();
        }

        if (address.Region != null && address.Region.Length > 0)
        {
            num = 5;
            array[4] = version == VCardVersion.vCard2_1 ? address.Region.RestrictedEscape() : address.Region.Escape();
        }

        if (address.PostalCode != null && address.PostalCode.Length > 0)
        {
            num = 6;
            array[5] = version == VCardVersion.vCard2_1 ? address.PostalCode.RestrictedEscape() : address.PostalCode.Escape();
        }

        if (address.Country != null && address.Country.Length > 0)
        {
            num = 7;
            array[6] = version == VCardVersion.vCard2_1 ? address.Country.RestrictedEscape() : address.Country.Escape();
        }

        return num == 0 ? null : Encode(address, string.Join(";", array));
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

        if (value.Length > 0)
        {
            var array = _reSplitSemiColon.Split(value);

            if (array.Length != 0)
            {
                address.POBox = array[0].Unescape();
            }

            if (array.Length > 1)
            {
                address.ExtendedAddress = array[1].Unescape();
            }

            if (array.Length > 2)
            {
                address.StreetAddress = array[2].Unescape();
            }

            if (array.Length > 3)
            {
                address.Locality = array[3].Unescape();
            }

            if (array.Length > 4)
            {
                address.Region = array[4].Unescape();
            }

            if (array.Length > 5)
            {
                address.PostalCode = array[5].Unescape();
            }

            if (array.Length > 6)
            {
                address.Country = array[6].Unescape();
            }
        }

        return address;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}
