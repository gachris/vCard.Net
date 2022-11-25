using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using vCard.Net.CardComponents;
using vCard.Net.Utility;

namespace vCard.Net.Serialization
{
    public class ComponentSerializer : SerializerBase
    {
        protected virtual IComparer<ICardProperty> PropertySorter => new PropertyAlphabetizer();

        public ComponentSerializer() { }

        public ComponentSerializer(SerializationContext ctx) : base(ctx) { }

        public override Type TargetType => typeof(CardComponent);

        public override string SerializeToString(object obj)
        {
            if (!(obj is ICardComponent c))
            {
                return null;
            }

            var sb = new StringBuilder();
            var upperName = c.Name.ToUpperInvariant();
            sb.Append(TextUtil.FoldLines($"BEGIN:{upperName}"));

            // Get a serializer factory
            var sf = GetService<ISerializerFactory>();

            // Sort the vCard properties in alphabetical order before serializing them!
            var properties = c.Properties.OrderBy(p => p.Name).ToList();

            // Serialize properties
            foreach (var p in properties)
            {
                // Get a serializer for each property.
                var serializer = sf.Build(p.GetType(), SerializationContext) as IStringSerializer;
                sb.Append(serializer.SerializeToString(p));
            }

            // Serialize child objects
            foreach (var child in c.Children)
            {
                // Get a serializer for each child object.
                var serializer = sf.Build(child.GetType(), SerializationContext) as IStringSerializer;
                sb.Append(serializer.SerializeToString(child));
            }

            sb.Append(TextUtil.FoldLines($"END:{upperName}"));
            return sb.ToString();
        }

        public override object Deserialize(TextReader tr) => null;

        public class PropertyAlphabetizer : IComparer<ICardProperty>
        {
            public int Compare(ICardProperty x, ICardProperty y)
            {
                if (x == y)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                return y == null
                    ? 1
                    : string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}