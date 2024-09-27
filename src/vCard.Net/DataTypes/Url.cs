using System;
using System.Collections.Generic;
using System.IO;
using vCard.Net.Serialization.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents a URL (URL) property of a vCard object.
/// </summary>
/// <remarks>
/// A URL property represents a Uniform Resource Locator (URL) associated with a vCard. It can be used to provide links to various resources,
/// such as a personal website, social media profile, or online portfolio.
/// </remarks>
public class Url : EncodableDataType
{
    /// <summary>
    /// Gets the versions of the vCard specification supported by this property.
    /// </summary>  
    /// <value>
    /// Supports all specifications.
    /// </value>
    public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;

    /// <summary>
    /// Gets or sets the list of types associated with this URL.
    /// </summary>
    public virtual IList<string> Types
    {
        get => Parameters.GetMany("TYPE");
        set => Parameters.Set("TYPE", value);
    }

    /// <summary>
    /// Gets or sets the preferred order for this URL (vCard 4.0 only).
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
    /// Gets or sets the value of the URL.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Url"/> class.
    /// </summary>
    public Url()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Url"/> class with the specified value.
    /// </summary>
    /// <param name="value">The URL value.</param>
    public Url(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new UrlSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }

    /// <summary>
    /// Determines whether the current <see cref="Url"/> object is equal to another <see cref="Url"/> object.
    /// </summary>
    /// <param name="other">The <see cref="Url"/> object to compare with the current object.</param>
    /// <returns>True if the current object is equal to the other object; otherwise, false.</returns>
    protected bool Equals(Url other)
    {
        return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase)
               && CollectionHelpers.Equals(Types, other.Types)
               && Equals(PreferredOrder, other.PreferredOrder);
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return !(obj is null) && (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Url)obj));
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            var hashCode = 17;
            hashCode = hashCode * 23 + (Value != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Value) : 0);
            hashCode = (hashCode * 23) ^ CollectionHelpers.GetHashCode(Types);
            hashCode = (hashCode * 23) ^ PreferredOrder.GetHashCode();
            return hashCode;
        }
    }
}
