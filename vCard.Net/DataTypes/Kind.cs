using System;
using System.IO;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes
{
    /// <summary>
    /// This class is used to represent the kind (KIND) property of a vCard. This specifies
    /// the type of entity represented by the vCard (see <see cref="CardKind"/>).
    /// </summary>
    public class Kind : EncodableDataType
    {
        private CardKind _cardKind;
        private string _otherKind;

        /// <summary>
        /// This is used to establish the specification versions supported by the PDI object
        /// </summary>
        /// <value>
        /// Supports vCard 4.0 only
        /// </value>
        public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard40;

        /// <summary>
        /// This property is used to set or get the vCard kind value
        /// </summary>
        /// <remarks>
        /// Setting this parameter to Other sets the <see cref="OtherKind"/>
        /// to X-UNKNOWN if not already set to something else.
        /// </remarks>
        public CardKind CardKind
        {
            get => _cardKind;
            set
            {
                _cardKind = value;
                if (_cardKind != CardKind.Other)
                {
                    _otherKind = null;
                }
                else if (string.IsNullOrWhiteSpace(_otherKind))
                {
                    _otherKind = "X-UNKNOWN";
                }
            }
        }

        /// <summary>
        /// This property is used to set or get the card kind string when the type is set to Other
        /// </summary>
        /// <value>
        /// Setting this parameter automatically sets the <see cref="CardKind"/> property to Other.
        /// </value>
        public string OtherKind
        {
            get => _otherKind;
            set
            {
                _cardKind = CardKind.Other;
                _otherKind = !string.IsNullOrWhiteSpace(value) ? value : "X-UNKNOWN";
            }
        }

        public Kind()
        {
            Version = SpecificationVersions.vCard40;
            CardKind = CardKind.None;
        }

        public Kind(string value)
        {
            Version = SpecificationVersions.vCard40;
            CardKind = CardKind.None;

            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            var serializer = new KindSerializer();
            CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
        }

        protected bool Equals(Kind other)
        {
            return Equals(CardKind, other.CardKind) && string.Equals(OtherKind, other.OtherKind, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            return obj is null ? false : ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Kind)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = CardKind.GetHashCode();
                hashCode = hashCode * 397 ^ (OtherKind?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}