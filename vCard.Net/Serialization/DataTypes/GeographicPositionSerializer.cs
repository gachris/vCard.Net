using System;
using System.Globalization;
using System.IO;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes
{
    public class GeographicPositionSerializer : StringSerializer
    {
        public GeographicPositionSerializer() : base()
        {
        }

        public GeographicPositionSerializer(SerializationContext ctx) : base(ctx)
        {
        }

        public override Type TargetType => typeof(GeographicPosition);

        public override string SerializeToString(object obj)
        {
            if (!(obj is GeographicPosition geographicPosition))
            {
                return null;
            }

            if (geographicPosition.Latitude == 0.0 && geographicPosition.Longitude == 0.0)
            {
                return null;
            }

            string text = string.Empty;
            if (geographicPosition.Version == SpecificationVersions.vCard40 && geographicPosition.IncludeGeoUriPrefix)
            {
                text = "geo:";
            }

            var value = string.Format(CultureInfo.InvariantCulture, "{0}{1:F6}{2}{3:F6}", text, geographicPosition.Latitude, (geographicPosition.Version == SpecificationVersions.vCard30 || geographicPosition.Version == SpecificationVersions.vCard40) ? ";" : ",", geographicPosition.Longitude);

            return Encode(geographicPosition, value);
        }

        public GeographicPosition Deserialize(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            if (!(CreateAndAssociate() is GeographicPosition geographicPosition))
            {
                return null;
            }

            // Decode the value, if necessary!
            value = Decode(geographicPosition, value);

            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            double num3 = (geographicPosition.Latitude = (geographicPosition.Longitude = 0.0));

            if (value.StartsWith("geo:", StringComparison.OrdinalIgnoreCase))
            {
                geographicPosition.IncludeGeoUriPrefix = true;
                value = value.Substring(4);
            }
            else
            {
                geographicPosition.IncludeGeoUriPrefix = false;
            }

            string[] array = value.Split(new char[2] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (array.Length == 2)
            {
                geographicPosition.Latitude = Convert.ToDouble(array[0], CultureInfo.InvariantCulture);
                geographicPosition.Longitude = Convert.ToDouble(array[1], CultureInfo.InvariantCulture);
            }

            return geographicPosition;
        }

        public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
    }
}