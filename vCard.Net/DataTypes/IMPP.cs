using System;
using System.Collections.Generic;
using System.IO;
using vCard.Net.Serialization.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents an Instant Messaging and Presence Protocol (IMPP) property in a vCard.
/// </summary>
public class IMPP : EncodableDataType
{
    /// <summary>
    /// Gets the versions of the vCard specification supported by this property.
    /// </summary>
    /// <value>
    /// Only supported by the vCard 3.0 and vCard 4.0 specification.
    /// </value>
    public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard30 | SpecificationVersions.vCard40;

    /// <summary>
    /// Gets or sets the list of social profile types associated with this IMPP property.
    /// </summary>
    public virtual IList<string> Types
    {
        get => Parameters.GetMany("TYPE");
        set => Parameters.Set("TYPE", value);
    }

    /// <summary>
    /// Gets or sets the preferred order for this IMPP (vCard 4.0 only).
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
    /// Gets or sets the value of the IMPP property.
    /// </summary>
    public virtual string Value { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IMPP"/> class.
    /// </summary>
    public IMPP()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IMPP"/> class with the specified value.
    /// </summary>
    /// <param name="value">The IMPP property value.</param>
    public IMPP(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new IMPPSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }

    /// <summary>
    /// Determines whether the current <see cref="IMPP"/> object is equal to another <see cref="IMPP"/> object.
    /// </summary>
    /// <param name="other">The <see cref="IMPP"/> object to compare with the current object.</param>
    /// <returns>True if the current object is equal to the other object; otherwise, false.</returns>
    protected bool Equals(IMPP other)
    {
        return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase)
               && CollectionHelpers.Equals(Types, other.Types)
               && Equals(PreferredOrder, other.PreferredOrder);
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj != null && (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((IMPP)obj));
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