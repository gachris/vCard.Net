using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents the Logo (LOGO) property of a vCard.
/// </summary>
/// <remarks>
/// Since it also represents an image, it provides features and functionality similar to the Photo property.
/// Only the property tag is different.
/// </remarks>
public class Logo : Photo
{
    /// <summary>
    /// Gets the versions of the vCard specification supported by this property.
    /// </summary>
    /// <value>
    /// Supports all vCard specifications.
    /// </value>
    public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;

    /// <summary>
    /// Initializes a new instance of the <see cref="Logo"/> class.
    /// </summary>
    public Logo()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Logo"/> class with the specified value.
    /// </summary>
    /// <param name="value">The logo value.</param>
    public Logo(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new LogoSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }
}
