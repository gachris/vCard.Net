using System;
using System.IO;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes
{
    public class KindSerializer : EncodableDataTypeSerializer
    {
        public KindSerializer() : base()
        {
        }

        public KindSerializer(SerializationContext ctx) : base(ctx)
        {
        }

        public override Type TargetType => typeof(Kind);

        public override string SerializeToString(object obj)
        {
            if (!(obj is Kind kind))
            {
                return null;
            }

            string cardKind;

            switch (kind.CardKind)
            {
                case CardKind.None:
                    return null;
                case CardKind.Other:
                    cardKind = kind.OtherKind;
                    break;
                case CardKind.Organization:
                    cardKind = "org";
                    break;
                default:
                    cardKind = kind.CardKind.ToString().ToLowerInvariant();
                    break;
            }

            return string.IsNullOrEmpty(cardKind) ? null : Encode(kind, cardKind);
        }

        public Kind Deserialize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new Kind();
            }

            if (!(CreateAndAssociate() is Kind kind))
            {
                return null;
            }

            // Decode the value, if necessary!
            value = Decode(kind, value);

            if (value != null)
            {
                string text = value.Trim().ToLowerInvariant();
                kind.OtherKind = null;
                switch (text)
                {
                    case "individual":
                        kind.CardKind = CardKind.Individual;
                        break;
                    case "group":
                        kind.CardKind = CardKind.Group;
                        break;
                    case "org":
                        kind.CardKind = CardKind.Organization;
                        break;
                    case "location":
                        kind.CardKind = CardKind.Location;
                        break;
                    default:
                        kind.OtherKind = text;
                        break;
                }
            }
            else
            {
                kind.CardKind = CardKind.None;
            }

            return kind;
        }

        public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
    }
}