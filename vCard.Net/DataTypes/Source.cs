using System;
using System.IO;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes
{
    /// <summary>
    /// A source of directory information for a vCard.
    /// </summary>
    /// <remarks>
    /// A source identifies a directory that contains or provided information for the
    /// vCard. A source consists of a URI and a context. The URI is generally a URL;
    /// the context identifies the protocol and type of URI. For example, a vCard associated
    /// with an LDAP directory entry will have an ldap:// URL and a context of "LDAP".
    /// </remarks>
    public class Source : EncodableDataType
    {
        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>
        /// Supports all vCard specifications
        /// </value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;

        /// <summary>
        /// This property is used to set or get a string containing the context of the property
        /// value such as a protocol for a URI.
        /// </summary>
        /// <value>
        /// The value is a string defining the context of the property value such as LDAP,
        /// HTTP, etc. It is only applicable to vCard 3.0 objects.
        /// </value>
        public string Context
        {
            get => Parameters.Get("CONTEXT");
            set
            {
                if (value is null)
                {
                    Parameters.Remove("CONTEXT");
                }
                else Parameters.Set("CONTEXT", value);
            }
        }

        public Uri Value { get; set; }

        /// <summary>
        /// Default constructor. Unless the version is changed, the object will conform to the vCard 3.0 specification.
        /// </summary>
        public Source()
        {
            Version = SpecificationVersions.vCard30;
        }

        public Source(string value)
        {
            Version = SpecificationVersions.vCard30;

            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            var serializer = new SourceSerializer();
            CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
        }

        protected bool Equals(Source other)
        {
            return string.Equals(Context, other.Context, StringComparison.OrdinalIgnoreCase) && Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            return !(obj is null) && (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Source)obj));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Context?.GetHashCode() ?? 0;
                hashCode = hashCode * 397 ^ (Value?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}