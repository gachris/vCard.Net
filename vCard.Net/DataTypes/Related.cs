using System.Collections.Generic;
using System.IO;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes;

/// <summary>
/// This class represents the Related (RELATED) property of an iCalendar component.
/// It indicates a relationship between the object and another object identified by
/// the unique ID in this property's value.
/// </summary>
/// <remarks>
/// The Related class is used to define relationships between iCalendar components,
/// particularly for referencing other components by their unique identifiers.
/// It provides properties for specifying the type of relationship and the preferred
/// order, particularly relevant for vCard 4.0 specifications.
/// </remarks>
public class Related : EncodableDataType
{
    /// <summary>
    /// Gets the versions of the vCard specification supported by this property.
    /// </summary>
    public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard40;

    /// <summary>
    /// Gets or sets the types associated with the related object.
    /// </summary>
    public virtual IList<string> Types
    {
        get => Parameters.GetMany("TYPE");
        set => Parameters.Set("TYPE", value);
    }

    /// <summary>
    /// Gets or sets the preferred order for this related object (vCard 4.0 only).
    /// </summary>
    /// <value>
    /// Zero if not set or the preferred usage order between 1 and 100.
    /// </value>
    public virtual short PreferredOrder
    {
        get
        {
            var preferredOrder = Parameters.Get("PREF");
            if (short.TryParse(preferredOrder, out short result))
            {
                return result;
            }

            return short.MinValue;
        }
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            else if (value > 100)
            {
                value = 100;
            }

            if (value > 0)
            {
                Parameters.Set("PREF", value.ToString());
            }
            else
            {
                Parameters.Remove("PREF");
            }
        }
    }

    /// <summary>
    /// Gets or sets the value of the related object.
    /// </summary>
    public virtual string Value { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Related"/> class.
    /// </summary>
    public Related()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Related"/> class with the specified value.
    /// </summary>
    /// <param name="value">The value representing the related object.</param>
    public Related(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new RelatedSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }
}