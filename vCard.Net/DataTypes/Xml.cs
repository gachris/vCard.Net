using System;
using System.IO;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents the Xml (XML) property of a vCard.
/// </summary>
/// <remarks>
/// This property class parses the <see cref="Value"/> property
/// and allows access to the component organization name and unit parts.
/// </remarks>
public class Xml : EncodableDataType
{
    /// <summary>
    /// Gets the versions of the vCard specification supported by this property.
    /// </summary>
    /// <value>
    /// Only supported by the vCard 4.0 specification.
    /// </value>
    public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard40;

    /// <summary>
    /// Gets or sets the value of the xml.
    /// </summary>
    public virtual string Value { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Xml"/> class.
    /// </summary>
    public Xml()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Xml"/> class with the specified value.
    /// </summary>
    /// <param name="value">The photo value.</param>
    public Xml(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new XmlSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }

    /// <summary>
    /// Determines whether the current <see cref="Xml"/> object is equal to another <see cref="Xml"/> object.
    /// </summary>
    /// <param name="other">The <see cref="Xml"/> object to compare with the current object.</param>
    /// <returns>True if the current object is equal to the other object; otherwise, false.</returns>
    protected bool Equals(Xml other)
    {
        return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj != null && (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Xml)obj));
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            var hashCode = 17;
            hashCode = hashCode * 23 + (Value != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Value) : 0);
            return hashCode;
        }
    }
}