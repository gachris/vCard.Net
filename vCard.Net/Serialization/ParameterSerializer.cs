using System;
using System.IO;
using System.Text;

namespace vCard.Net.Serialization
{
    public class ParameterSerializer : SerializerBase
    {
        public ParameterSerializer() { }

        public ParameterSerializer(SerializationContext ctx) : base(ctx) { }

        public override Type TargetType => typeof(CardParameter);

        public override string SerializeToString(object obj)
        {
            if (!(obj is CardParameter p))
            {
                return null;
            }

            var builder = new StringBuilder();
            builder.Append(p.Name + "=");

            // "Section 3.2:  Property parameter values MUST NOT contain the DQUOTE character."
            // Therefore, let's strip any double quotes from the value.
            var values = string.Join(",", p.Values).Replace("\"", string.Empty);

            // Surround the parameter value with double quotes, if the value
            // contains any problematic characters.
            if (values.IndexOfAny(new[] { ';', ':', ',' }) >= 0)
            {
                values = "\"" + values + "\"";
            }
            builder.Append(values);
            return builder.ToString();
        }

        public override object Deserialize(TextReader tr) => null;
    }
}