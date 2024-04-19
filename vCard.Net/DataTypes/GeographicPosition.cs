using System.IO;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents the geographic position (GEO) property of a vCard.
/// </summary>
/// <remarks>
/// This property class parses the <see cref="Latitude"/> and the <see cref="Longitude"/> property
/// to allow access to its individual latitude and longitude parts as numeric values.
/// For the vCard 2.1 specification, the values are separated by
/// a comma. For the vCard 3.0 they are separated
/// by a semi-colon.
/// </remarks>
public class GeographicPosition : EncodableDataType
{
    /// <summary>
    /// Gets the specification versions supported by the GeographicPosition object.
    /// </summary>
    /// <value>
    /// Supports all specifications.
    /// </value>
    public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;

    /// <summary>
    /// Gets or sets the latitude as a floating point value.
    /// </summary>
    /// <value>
    /// Positive values indicate positions north of the equator. Negative values indicate
    /// positions south of the equator.
    /// </value>
    public double Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude as a floating point value.
    /// </summary>
    /// <remarks>
    /// Positive values indicate positions east of the prime meridian. Negative values
    /// indicate positions west of the prime meridian.
    /// </remarks>
    public double Longitude { get; set; }

    /// <summary>
    /// Gets or sets whether or not to include the "geo:" URI prefix when
    /// saving the property value in vCard 4.0 files.
    /// </summary>
    /// <value>
    /// The default is true.
    /// </value>
    public bool IncludeGeoUriPrefix { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GeographicPosition"/> class.
    /// </summary>
    public GeographicPosition()
    {
        IncludeGeoUriPrefix = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GeographicPosition"/> class with the specified value.
    /// </summary>
    /// <param name="value">The geographic position value.</param>
    public GeographicPosition(string value)
    {
        IncludeGeoUriPrefix = true;

        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new GeographicPositionSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }
}