using System;
using System.IO;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes
{
    public class KeySerializer : EncodableDataTypeSerializer
    {
        public KeySerializer() : base()
        {
        }

        public KeySerializer(SerializationContext ctx) : base(ctx)
        {
        }

        public override Type TargetType => typeof(Key);

        public override string SerializeToString(object obj)
        {
            if (!(obj is Key key))
            {
                return null;
            }

            //if (!string.IsNullOrWhiteSpace(KeyType))
            //{
            //    sb.Append(';');
            //    if (Version != SpecificationVersions.vCard21)
            //    {
            //        sb.Append("TYPE");
            //        sb.Append('=');
            //    }

            //    sb.Append(KeyType);
            //}

            if (!string.IsNullOrWhiteSpace(key.KeyType))
            {
                key.Parameters.Set("TYPE", key.KeyType);
            }
            else if (key.Parameters.ContainsKey("TYPE"))
            {
                key.Parameters.Remove("TYPE");
            }

            return Encode(key, key.Value);
        }

        public Key Deserialize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            if (!(CreateAndAssociate() is Key key))
            {
                return null;
            }

            // Decode the value, if necessary!
            value = Decode(key, value);

            if (value is null)
            {
                return null;
            }

            key.Value = value;
            key.KeyType = key.Parameters.Get("TYPE");

            return key;
        }

        public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
    }
}