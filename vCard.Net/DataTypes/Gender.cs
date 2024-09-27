using System;
using System.IO;
using vCard.Net.Serialization.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents the gender (GENDER) property of a vCard object.
/// </summary>
/// <remarks>
/// This property class parses the <see cref="Sex"/> and the <see cref="GenderIdentity"/> property
/// to allow access to its individual sex and gender identity parts. This property
/// is only valid for use with the vCard 4.0 specification.
/// </remarks>
public class Gender : EncodableDataType
{
    /// <summary>
    /// Gets the versions of the vCard specification supported by this property.
    /// </summary>
    /// <value>
    /// Only supported by the vCard 4.0 specification.
    /// </value>
    public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard4_0;

    /// <summary>
    /// Gets or sets the sex.
    /// </summary>
    public char? Sex { get; set; }

    /// <summary>
    /// Gets or sets the gender identity.
    /// </summary>
    public string GenderIdentity { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gender"/> class.
    /// </summary>
    public Gender()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gender"/> class with the specified value.
    /// </summary>
    /// <param name="value">The gender value.</param>
    public Gender(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new GenderSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }

    /// <summary>
    /// Determines whether the current <see cref="Gender"/> object is equal to another <see cref="Gender"/> object.
    /// </summary>
    /// <param name="other">The <see cref="Gender"/> object to compare with the current object.</param>
    /// <returns>True if the current object is equal to the other object; otherwise, false.</returns>
    protected bool Equals(Gender other)
    {
        return string.Equals(GenderIdentity, other.GenderIdentity, StringComparison.OrdinalIgnoreCase)
               && Equals(Sex, other.Sex);
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj != null && (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Gender)obj));
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            var hashCode = 17;
            hashCode = hashCode * 23 + (GenderIdentity != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(GenderIdentity) : 0);
            hashCode = (hashCode * 23) ^ (Sex?.GetHashCode() ?? 0);
            return hashCode;
        }
    }
}