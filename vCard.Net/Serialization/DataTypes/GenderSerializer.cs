using System;
using System.IO;
using System.Linq;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes
{
    public class GenderSerializer : StringSerializer
    {
        public GenderSerializer() : base()
        {
        }

        public GenderSerializer(SerializationContext ctx) : base(ctx)
        {
        }

        public override Type TargetType => typeof(Gender);

        public override string SerializeToString(object obj)
        {
            if (!(obj is Gender gender))
            {
                return null;
            }

            if (!gender.Sex.HasValue && string.IsNullOrWhiteSpace(gender.GenderIdentity))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(gender.GenderIdentity))
            {
                return gender.Sex.ToString();
            }

            return Encode(gender, string.Join(";", gender.Sex?.ToString(), gender.GenderIdentity));
        }

        public Gender Deserialize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            if (!(CreateAndAssociate() is Gender gender))
            {
                return null;
            }

            // Decode the value, if necessary!
            value = Decode(gender, value);

            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            gender.Sex = null;
            gender.GenderIdentity = null;
            string[] array = value.Split(new char[1] { ';' });
            if (array[0].Length != 0)
            {
                gender.Sex = array[0][0];
                if (!new char[5] { 'M', 'F', 'O', 'N', 'U' }.Contains(gender.Sex.Value))
                {
                    gender.Sex = null;
                }
            }

            if (array.Length > 1)
            {
                gender.GenderIdentity = array[1];
            }

            return gender;
        }

        public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
    }
}