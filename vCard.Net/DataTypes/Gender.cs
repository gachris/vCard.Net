using System.IO;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes
{
    /// <summary>
    /// This class is used to represent the gender (GENDER) property of a vCard object
    /// </summary>
    /// <remarks>
    /// This property class parses the <see cref="Sex"/> and the <see cref="GenderIdentity"/> property
    /// to allow access to its individual sex and gender identity parts. This property
    /// is only valid for use with the vCard 4.0 specification.
    /// </remarks>
    public class Gender : EncodableDataType
    {
        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>
        /// Only supported by the vCard 4.0 specification
        /// </value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard40;

        /// <summary>
        /// This is used to get or set the sex
        /// </summary>
        public char? Sex { get; set; }

        /// <summary>
        /// This is used to get or set the gender identity
        /// </summary>
        public string GenderIdentity { get; set; }

        public Gender()
        {
        }

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
}