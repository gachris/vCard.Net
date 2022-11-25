using System.IO;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes
{
    /// <summary>
    /// This class is used to represent the geographic position (GEO) property of a vCard
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
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>
        /// Supports all specifications
        /// </value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;

        /// <summary>
        /// This is used to get or set the latitude as a floating point value
        /// </summary>
        /// <value>
        /// Positive values indicate positions north of the equator. Negative values indicate
        /// positions south of the equator.
        /// </value>
        public double Latitude { get; set; }

        /// <summary>
        /// This is used to get or set the longitude as a floating point value
        /// </summary>
        /// <remarks>
        /// Positive values indicate positions east of the prime meridian. Negative values
        /// indicate positions west of the prime meridian.
        /// </remarks>
        public double Longitude { get; set; }

        /// <summary>
        /// This is used to get or set whether or not to include the "geo:" URI prefix when
        /// saving the property value in vCard 4.0 files.
        /// </summary>
        /// <value>
        /// The default is true
        /// </value>
        public bool IncludeGeoUriPrefix { get; set; }

        public GeographicPosition()
        {
            Version = SpecificationVersions.vCard30;
            IncludeGeoUriPrefix = true;
        }

        public GeographicPosition(string value)
        {
            Version = SpecificationVersions.vCard30;
            IncludeGeoUriPrefix = true;

            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            var serializer = new GeographicPositionSerializer();
            CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
        }
    }
}