using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using vCard.Net.CardComponents;
using vCard.Net.Utility;

namespace vCard.Net.Serialization;

/// <summary>
/// Serializer for serializing vCard components to string format.
/// </summary>
public class ComponentSerializer : SerializerBase
{
    /// <summary>
    /// Gets or sets the property sorter used for sorting vCard properties alphabetically.
    /// </summary>
    protected virtual IComparer<IvCardProperty> PropertySorter => new PropertyAlphabetizer();

    /// <summary>
    /// Initializes a new instance of the <see cref="ComponentSerializer"/> class.
    /// </summary>
    public ComponentSerializer() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ComponentSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public ComponentSerializer(SerializationContext ctx) : base(ctx) { }

    /// <inheritdoc/>
    public override Type TargetType => typeof(vCardComponent);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        if (!(obj is IvCardComponent c))
        {
            return null;
        }

        var sb = new StringBuilder();
        var upperName = c.Name.ToUpperInvariant();
        sb.Append(TextUtil.FoldLines($"BEGIN:{upperName}"));
        sb.Append(TextUtil.FoldLines($"VERSION:{c.Version.ToVersionString()}"));

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

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => null;

    /// <summary>
    /// A comparer implementation for sorting vCard properties alphabetically.
    /// </summary>
    public class PropertyAlphabetizer : IComparer<IvCardProperty>
    {
        /// <inheritdoc/>
        public int Compare(IvCardProperty x, IvCardProperty y)
        {
            return x == y ? 0 : x == null ? -1 : y == null ? 1 : string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
        }
    }
}
