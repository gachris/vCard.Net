﻿using vCard.Net.Serialization.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents the language (LANGUAGE) property of a vCard. This specifies the language of the vCard content.
/// </summary>
public class Language : EncodableDataType
{
    /// <summary>
    /// Gets or sets the list of types associated with this language.
    /// </summary>
    public virtual IList<string> Types
    {
        get => Parameters.GetMany("TYPE");
        set => Parameters.Set("TYPE", value);
    }

    /// <summary>
    /// Gets or sets the preferred order for this language (vCard 4.0 only).
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
    /// Gets or sets the value of the language.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Language"/> class.
    /// </summary>
    public Language()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Language"/> class with the specified value.
    /// </summary>
    /// <param name="value">The language value as a string.</param>
    public Language(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new LanguageSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }

    /// <summary>
    /// Determines whether the current <see cref="Language"/> object is equal to another <see cref="Language"/> object.
    /// </summary>
    /// <param name="other">The <see cref="Language"/> object to compare with the current object.</param>
    /// <returns>True if the current object is equal to the other object; otherwise, false.</returns>
    protected bool Equals(Language other)
    {
        return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase)
               && CollectionHelpers.Equals(Types, other.Types)
               && Equals(PreferredOrder, other.PreferredOrder);
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj != null && (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Language)obj));
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