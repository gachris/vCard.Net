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
}
