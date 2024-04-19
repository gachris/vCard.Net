using System.IO;
using vCard.Net.Serialization.DataTypes;

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
    /// Gets the specification versions supported by the Gender object.
    /// </summary>
    /// <value>
    /// Only supported by the vCard 4.0 specification.
    /// </value>
    public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard40;

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
}