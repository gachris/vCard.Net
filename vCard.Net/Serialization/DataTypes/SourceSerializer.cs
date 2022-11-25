using System;
using System.IO;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes
{
    public class SourceSerializer : StringSerializer
    {
        public SourceSerializer() : base()
        {
        }

        public SourceSerializer(SerializationContext ctx) : base(ctx)
        {
        }

        public override Type TargetType => typeof(Source);

        public override string SerializeToString(object obj)
        {
            if (!(obj is Source source))
            {
                return null;
            }

            try
            {
                return source?.Value == null ? null : Encode(source, Escape(source.Value.OriginalString));
            }
            catch
            {
                return null;
            }
        }

        public override object Deserialize(TextReader tr)
        {
            var value = tr.ReadToEnd();

            Source source = null;
            try
            {
                source = CreateAndAssociate() as Source;
                if (source != null)
                {
                    var uriString = Unescape(Decode(source, value));
                    source.Value = new Uri(uriString);
                }
            }
            catch { }

            return source;
        }
    }
}