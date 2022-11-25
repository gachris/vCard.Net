using System.IO;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes
{
    /// <summary>
    /// This class is used to represent the client PID map (CLIENTPIDMAP) property of a vCard object
    /// </summary>
    /// <remarks>
    /// This property class parses the <see cref="Uri"/> property
    /// to allow access to its individual property ID and URI parts. This property is
    /// only valid for use with the vCard 4.0 specification.
    /// </remarks>
    public class ClientPidMap : EncodableDataType
    {
        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>
        /// Only supported by the vCard 4.0 specification
        /// </value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard40;

        /// <summary>
        /// This is used to get or set the property ID number
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// This is used to get or set the URI
        /// </summary>
        public string Uri { get; set; }

        public ClientPidMap()
        {
        }

        public ClientPidMap(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            var serializer = new ClientPidMapSerializer();
            CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
        }
    }
}