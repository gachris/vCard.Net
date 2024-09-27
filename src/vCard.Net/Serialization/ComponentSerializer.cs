using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using vCard.Net.CardComponents;
using vCard.Net.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.Serialization;

/// <summary>
/// Serializer for serializing vCard components to string format.
/// </summary>
public class ComponentSerializer : SerializerBase
{
    private static readonly Dictionary<string, SpecificationVersions> _specificationVersions = new()
    {
        { "UID", SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Address
        { "VERSION", SpecificationVersions.vCard2_1 | SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Address
        { "ADR", SpecificationVersions.vCard2_1 | SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Address
        { "AGENT", SpecificationVersions.vCard2_1 | SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Agent
        { "ANNIVERSARY", SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Anniversary
        { "CATEGORIES", SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Categories
        { "CLIENTPIDMAP", SpecificationVersions.vCard2_1 | SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // ClientPidMap
        { "EMAIL", SpecificationVersions.vCard2_1 | SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Email
        { "EXPERTISE", SpecificationVersions.vCard4_0 }, // Expertise
        { "FN", SpecificationVersions.vCard2_1 | SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Formatted Name
        { "GENDER", SpecificationVersions.vCard4_0 }, // Gender
        { "GEO", SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // GeographicPosition
        { "HOBBY", SpecificationVersions.vCard4_0 }, // Hobby
        { "IMPP", SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // IMPP
        { "INTEREST", SpecificationVersions.vCard4_0 }, // Interest
        { "KEY", SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Key
        { "KIND", SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Kind
        { "LABEL",SpecificationVersions.vCard2_1 | SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Label
        { "LANG", SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Language
        { "LOGO", SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Logo
        { "MEMBER", SpecificationVersions.vCard4_0 }, // Members
        { "MAILER", SpecificationVersions.vCard2_1 | SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Mailer
        { "NOTE", SpecificationVersions.vCard2_1 | SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Note
        { "NICKNAME", SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Nickname
        { "N", SpecificationVersions.vCard2_1 | SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Structured Name
        { "ORG", SpecificationVersions.vCard2_1 | SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Organization
        { "PHOTO", SpecificationVersions.vCard2_1 | SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Photo
        { "PRODID", SpecificationVersions.vCard2_1 | SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Product Id
        { "REV", SpecificationVersions.vCard2_1 | SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Revision
        { "RELATED", SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Related
        { "ROLE", SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Role
        { "SOUND", SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Sound
        { "SOURCE", SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Source
        { "TEL", SpecificationVersions.vCard2_1 | SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Telephone
        { "TITLE", SpecificationVersions.vCard2_1 | SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Title
        { "TZ", SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Time Zone
        { "URL", SpecificationVersions.vCard2_1 | SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Url
        { "XML", SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Xml
        { "BIRTHPLACE", SpecificationVersions.vCard4_0 }, // Birthplace
        { "DEATHPLACE", SpecificationVersions.vCard4_0 }, // Deathplace
        { "DEATHDATE", SpecificationVersions.vCard4_0 }, // Deathdate
        { "BDAY", SpecificationVersions.vCard2_1 | SpecificationVersions.vCard3_0 | SpecificationVersions.vCard4_0 }, // Birthday
    };

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

        var vcardversion = c.Version.ToString();
        var vcardSpecificationVersions = (SpecificationVersions)Enum.Parse(typeof(SpecificationVersions), vcardversion);

        // Get a serializer factory
        var sf = GetService<ISerializerFactory>();

        // Sort the vCard properties in alphabetical order before serializing them!
        // Filter and sort the vCard properties in alphabetical order before serializing them!
        var properties = c.Properties
            .Where(p => _specificationVersions.ContainsKey(p.Name.ToUpperInvariant())
                        && (_specificationVersions[p.Name.ToUpperInvariant()] & vcardSpecificationVersions) != 0) // Filter valid properties based on version
            .OrderBy(p => p, PropertySorter)
            .ToList();

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
    /// A comparer implementation for sorting vCard properties alphabetically,
    /// ensuring "LABEL" is always placed after "ADR" and "AGENT" is always at the bottom.
    /// </summary>
    public class PropertyAlphabetizer : IComparer<IvCardProperty>
    {
        /// <inheritdoc/>
        public int Compare(IvCardProperty x, IvCardProperty y)
        {
            // Handle null cases first
            if (x == y) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            // Check if one of the properties is "VERSION" and give it priority
            const string versionPropertyName = "VERSION";
            const string addressPropertyName = "ADR";
            const string labelPropertyName = "LABEL";
            const string agentPropertyName = "AGENT";

            // If one property is "VERSION", prioritize it
            if (string.Equals(x.Name, versionPropertyName, StringComparison.OrdinalIgnoreCase))
            {
                return -1; // x is "VERSION", it should come first
            }
            if (string.Equals(y.Name, versionPropertyName, StringComparison.OrdinalIgnoreCase))
            {
                return 1; // y is "VERSION", so it should come first
            }

            // Handle the specific case for "ADR" and "LABEL"
            if (string.Equals(x.Name, addressPropertyName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(y.Name, labelPropertyName, StringComparison.OrdinalIgnoreCase))
            {
                return -1; // "ADR" should come before "LABEL"
            }

            if (string.Equals(x.Name, labelPropertyName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(y.Name, addressPropertyName, StringComparison.OrdinalIgnoreCase))
            {
                return 1; // "LABEL" should come after "ADR"
            }

            // Keep "AGENT" at the bottom
            if (string.Equals(x.Name, agentPropertyName, StringComparison.OrdinalIgnoreCase))
            {
                return 1; // x is "AGENT", it should come after y
            }

            if (string.Equals(y.Name, agentPropertyName, StringComparison.OrdinalIgnoreCase))
            {
                return -1; // y is "AGENT", so it should come after x
            }

            // For other properties, sort alphabetically by their name
            return string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
        }
    }

}