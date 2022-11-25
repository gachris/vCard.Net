using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes
{
    public class OrganizationSerializer : StringSerializer
    {
        private static readonly Regex _reSplit = new Regex("(?:^[;])|(?<=(?:[^\\\\]))[;]");

        public OrganizationSerializer() : base()
        {
        }

        public OrganizationSerializer(SerializationContext ctx) : base(ctx)
        {
        }

        public override Type TargetType => typeof(Organization);

        public override string SerializeToString(object obj)
        {
            if (!(obj is Organization organization))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(organization.Name) && organization.Units.Count == 0)
            {
                return null;
            }

            var stringBuilder = new StringBuilder(256);

            stringBuilder.Append(organization.Name);

            foreach (string unit in organization.Units)
            {
                stringBuilder.Append(';');
                stringBuilder.Append(unit);
            }

            return Encode(organization, stringBuilder.ToString());
        }

        public Organization Deserialize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            if (!(CreateAndAssociate() is Organization organization))
            {
                return null;
            }

            // Decode the value, if necessary!
            value = Decode(organization, value);

            if (value is null)
            {
                return null;
            }

            organization.Name = null;
            organization.Units.Clear();

            string[] array = _reSplit.Split(value);
            organization.Name = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                string text = array[i].Trim();
                if (text.Length > 0)
                {
                    organization.Units.Add(text);
                }
            }

            return organization;
        }

        public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
    }
}