using System;
using System.Collections.Generic;
using System.IO;
using vCard.Net.Serialization.DataTypes;

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
    /// <summary>
    /// Gets the versions of the vCard specification supported by this property.
    /// </summary>  
    /// <value>
    /// Supports all specifications.
    /// </value>
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
            return short.TryParse(preferredOrder, out short result) ? result : short.MinValue;
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
    /// Gets the address value, parsing and concatenating the components when requested.
    /// </summary>
    /// <value>
    /// The component parts are escaped as needed.
    /// </value>
    public virtual string Value
    {
        get
        {
            string[] array = new string[8];
            int num = 0;
            if (POBox != null && POBox.Length > 0)
            {
                num = 1;
                array[0] = POBox;
            }

            if (ExtendedAddress != null && ExtendedAddress.Length > 0)
            {
                num = 2;
                array[1] = ExtendedAddress;
            }

            if (StreetAddress != null && StreetAddress.Length > 0)
            {
                num = 3;
                array[2] = StreetAddress;
            }

            if (Locality != null && Locality.Length > 0)
            {
                num = 4;
                array[3] = Locality;
            }

            if (Region != null && Region.Length > 0)
            {
                num = 5;
                array[4] = Region;
            }

            if (PostalCode != null && PostalCode.Length > 0)
            {
                num = 6;
                array[5] = PostalCode;
            }

            if (Country != null && Country.Length > 0)
            {
                num = 7;
                array[6] = Country;
            }

            return num == 0 ? null : string.Join(";", array, 0, num);
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

    /// <summary>
    /// Determines whether the current <see cref="Address"/> object is equal to another <see cref="Address"/> object.
    /// </summary>
    /// <param name="other">The <see cref="Address"/> object to compare with the current object.</param>
    /// <returns>True if the current object is equal to the other object; otherwise, false.</returns>
    protected bool Equals(Address other)
    {
        return string.Equals(POBox, other.POBox, StringComparison.OrdinalIgnoreCase)
               && string.Equals(ExtendedAddress, other.ExtendedAddress, StringComparison.OrdinalIgnoreCase)
               && string.Equals(StreetAddress, other.StreetAddress, StringComparison.OrdinalIgnoreCase)
               && string.Equals(Locality, other.Locality, StringComparison.OrdinalIgnoreCase)
               && string.Equals(Region, other.Region, StringComparison.OrdinalIgnoreCase)
               && string.Equals(PostalCode, other.PostalCode, StringComparison.OrdinalIgnoreCase)
               && string.Equals(Country, other.Country, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj != null && (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Address)obj));
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            var hashCode = 17;
            hashCode = hashCode * 23 + (POBox != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(POBox) : 0);
            hashCode = hashCode * 23 + (ExtendedAddress != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(ExtendedAddress) : 0);
            hashCode = hashCode * 23 + (StreetAddress != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(StreetAddress) : 0);
            hashCode = hashCode * 23 + (Locality != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Locality) : 0);
            hashCode = hashCode * 23 + (Region != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Region) : 0);
            hashCode = hashCode * 23 + (PostalCode != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(PostalCode) : 0);
            hashCode = hashCode * 23 + (Country != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Country) : 0);
            return hashCode;
        }
    }
}