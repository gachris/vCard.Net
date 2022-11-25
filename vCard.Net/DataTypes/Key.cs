using System.IO;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes
{
    // Remarks:

    /// <summary>
    /// This class is used to represent the Public Key (KEY) property of a vCard
    /// </summary>
    /// <remarks>
    /// The <see cref="Value"/> property contains the key value
    /// in string form. There is limited support for this property. It will decode the
    /// key type parameter and make it accessible through the <see cref="KeyType"/> property.
    /// </remarks>
    public class Key : EncodableDataType
    {
        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>
        /// Supports all vCard specifications
        /// </value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;

        /// <summary>
        /// This is used to set or get the public key type
        /// </summary>
        /// <value>
        /// The value is a string defining the type of key that the property value represents such as X509, PGP, etc.
        /// </value>
        public string KeyType { get; set; }

        public string Value { get; set; }

        public Key()
        {
        }

        public Key(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            var serializer = new KeySerializer();
            CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
        }
    }
}