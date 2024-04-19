using System.IO;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents the Photo (PHOTO) property of a vCard.
/// </summary>
/// <remarks>
/// This property class parses the <see cref="Name"/> property
/// and allows access to the component organization name and unit parts. This property
/// is based on the X.520 Organization Name attribute and the X.520 Organization
/// Unit attribute.
/// </remarks>
public class Photo : EncodableDataType
{
    /// <summary>
    /// This is used to establish the specification versions supported by the PDI object
    /// </summary>
    /// <value>
    /// Supports all vCard specifications
    /// </value>
    public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;

    /// <summary>
    /// Gets or sets the value of the photo.
    /// </summary>
    public virtual string Value { get; set; }

    /// <summary>
    /// Constructor. Unless the version is changed, the object will conform to the vCard 3.0 specification.
    /// </summary>
    public Photo()
    {
    }

    /// <summary>
    /// Constructor. Unless the version is changed, the object will conform to the vCard 3.0 specification.
    /// </summary>
    public Photo(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new PhotoSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }
}