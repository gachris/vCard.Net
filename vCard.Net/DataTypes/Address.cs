using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using vCard.Net.Serialization.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents the Address (ADR) property of a vCard object.
/// </summary>
/// <remarks>
/// This property class parses the <see cref="Value"/>
/// property and allows access to the component parts. This property is valid for use with
/// all vCard specification objects.
/// </remarks>
public class Address : EncodableDataType
{
    private static readonly Regex _reSplitSemiColon = new Regex("(?:^[;])|(?<=(?:[^\\\\]))[;]");

    /// <summary>
    /// Gets the versions of the vCard specification supported by this property.
    /// </summary>
    public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;

    /// <summary>
    /// Gets or sets the list of types associated with the address.
    /// </summary>
    public virtual IList<string> Types
    {
        get => Parameters.GetMany("TYPE");
        set => Parameters.Set("TYPE", value);
    }

    /// <summary>
    /// Gets or sets the preferred order (vCard 4.0 only).
    /// </summary>
    /// <value>
    /// Zero if not set or the preferred usage order between 1 and 100.
    /// </value>
    public virtual short PreferredOrder
    {
        get
        {
            var preferredOrder = Parameters.Get("PREF");
            if (short.TryParse(preferredOrder, out short result))
            {
                return result;
            }

            return short.MinValue;
        }
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            else if (value > 100)
            {
                value = 100;
            }

            if (value > 0)
            {
                Parameters.Set("PREF", value.ToString());
            }
            else
            {
                Parameters.Remove("PREF");
            }
        }
    }

    /// <summary>
    /// Gets or sets the PO Box.
    /// </summary>
    public virtual string POBox { get; set; }

    /// <summary>
    /// Gets or sets the extended address.
    /// </summary>
    public virtual string ExtendedAddress { get; set; }

    /// <summary>
    /// Gets or sets the street address.
    /// </summary>
    public virtual string StreetAddress { get; set; }

    /// <summary>
    /// Gets or sets the locality (city).
    /// </summary>
    public virtual string Locality { get; set; }

    /// <summary>
    /// Gets or sets the region (state or province).
    /// </summary>
    public virtual string Region { get; set; }

    /// <summary>
    /// Gets or sets the postal (zip) code.
    /// </summary>
    public virtual string PostalCode { get; set; }

    /// <summary>
    /// Gets or sets the country.
    /// </summary>
    public virtual string Country { get; set; }

    /// <summary>
    /// Gets or sets the address value, parsing and concatenating the components when requested.
    /// </summary>
    /// <value>
    /// The component parts are escaped as needed.
    /// </value>
    public virtual string Value
    {
        get
        {
            vCardVersion specificationVersions = Version;
            string[] array = new string[8];
            int num = 0;
            if (POBox != null && POBox.Length > 0)
            {
                num = 1;
                array[0] = specificationVersions == vCardVersion.vCard21 ? POBox.RestrictedEscape() : POBox.Escape();
            }

            if (ExtendedAddress != null && ExtendedAddress.Length > 0)
            {
                num = 2;
                array[1] = specificationVersions == vCardVersion.vCard21 ? ExtendedAddress.RestrictedEscape() : ExtendedAddress.Escape();
            }

            if (StreetAddress != null && StreetAddress.Length > 0)
            {
                num = 3;
                array[2] = specificationVersions == vCardVersion.vCard21 ? StreetAddress.RestrictedEscape() : StreetAddress.Escape();
            }

            if (Locality != null && Locality.Length > 0)
            {
                num = 4;
                array[3] = specificationVersions == vCardVersion.vCard21 ? Locality.RestrictedEscape() : Locality.Escape();
            }

            if (Region != null && Region.Length > 0)
            {
                num = 5;
                array[4] = specificationVersions == vCardVersion.vCard21 ? Region.RestrictedEscape() : Region.Escape();
            }

            if (PostalCode != null && PostalCode.Length > 0)
            {
                num = 6;
                array[5] = specificationVersions == vCardVersion.vCard21 ? PostalCode.RestrictedEscape() : PostalCode.Escape();
            }

            if (Country != null && Country.Length > 0)
            {
                num = 7;
                array[6] = specificationVersions == vCardVersion.vCard21 ? Country.RestrictedEscape() : Country.Escape();
            }

            return num == 0 ? null : string.Join(";", array, 0, num);
        }
        set
        {
            if (value != null && value.Length > 0)
            {
                var array = _reSplitSemiColon.Split(value);

                if (array.Length != 0)
                {
                    POBox = array[0].Unescape();
                }

                if (array.Length > 1)
                {
                    ExtendedAddress = array[1].Unescape();
                }

                if (array.Length > 2)
                {
                    StreetAddress = array[2].Unescape();
                }

                if (array.Length > 3)
                {
                    Locality = array[3].Unescape();
                }

                if (array.Length > 4)
                {
                    Region = array[4].Unescape();
                }

                if (array.Length > 5)
                {
                    PostalCode = array[5].Unescape();
                }

                if (array.Length > 6)
                {
                    Country = array[6].Unescape();
                }
            }
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Address"/> class.
    /// </summary>
    public Address()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Address"/> class with the specified value.
    /// </summary>
    /// <param name="value">The address value.</param>
    public Address(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new AddressSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }
}