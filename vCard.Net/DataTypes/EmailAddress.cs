﻿using System;
using System.Collections.Generic;
using System.IO;
using vCard.Net.Serialization.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents the E-mail address (EMAIL) property of a vCard object.
/// </summary>
/// <remarks>
/// This property is valid for use with all vCard specification objects.
/// </remarks>
public class EmailAddress : EncodableDataType
{
    /// <summary>
    /// Gets the versions of the vCard specification supported by this property.
    /// </summary>
    /// <value>
    /// Supports all specifications.
    /// </value>
    public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;

    /// <summary>
    /// Gets or sets the list of types associated with this email address.
    /// </summary>
    public virtual IList<string> Types
    {
        get => Parameters.GetMany("TYPE");
        set => Parameters.Set("TYPE", value);
    }

    /// <summary>
    /// Gets or sets the preferred order for this email address (vCard 4.0 only).
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
    /// Gets or sets the value of the email address.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAddress"/> class.
    /// </summary>
    public EmailAddress()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAddress"/> class with the specified value.
    /// </summary>
    /// <param name="value">The email address value.</param>
    public EmailAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new EmailAddressSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }

    /// <summary>
    /// Determines whether the current <see cref="EmailAddress"/> object is equal to another <see cref="EmailAddress"/> object.
    /// </summary>
    /// <param name="other">The <see cref="EmailAddress"/> object to compare with the current object.</param>
    /// <returns>True if the current object is equal to the other object; otherwise, false.</returns>
    protected bool Equals(EmailAddress other)
    {
        return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase)
               && CollectionHelpers.Equals(Types, other.Types)
               && Equals(PreferredOrder, other.PreferredOrder);
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj != null && (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((EmailAddress)obj));
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