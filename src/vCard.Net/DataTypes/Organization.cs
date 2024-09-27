using System.Collections.Specialized;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents the Organization Name and Unit (ORG) property of a vCard.
/// </summary>
/// <remarks>
/// This property class parses the <see cref="Name"/> property
/// and allows access to the component organization name and unit parts. This property
/// is based on the X.520 Organization Name attribute and the X.520 Organization
/// Unit attribute.
/// </remarks>
public class Organization : EncodableDataType
{
    private readonly StringCollection _units;

    /// <summary>
    /// Gets the versions of the vCard specification supported by this property.
    /// </summary>  
    /// <value>
    /// Supports all specifications.
    /// </value>
    public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;

    /// <summary>
    /// Gets or sets the organization name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the string to use when sorting organizations.
    /// </summary>
    /// <value>
    /// If set, this value should be given precedence over the <see cref="Name"/> property.
    /// </value>
    public string SortAs
    {
        get => Parameters.Get("SORT-AS");
        set
        {
            if (value is null)
            {
                Parameters.Remove("SORT-AS");
            }
            else Parameters.Set("SORT-AS", value);
        }
    }

    /// <summary>
    /// Gets the Organization Units string collection.
    /// </summary>
    /// <value>
    /// Units can be added to or removed from the returned collection reference.
    /// </value>
    public StringCollection Units => _units;

    /// <summary>
    /// Sets or gets the Organization Units as a string value.
    /// </summary>
    /// <remarks>
    /// The string can contain one or more unit names separated by commas or semi-colons.
    /// The string will be split and loaded into the units string collection.
    /// </remarks>
    public string UnitsString
    {
        get => string.Join(", ", _units.OfType<string>());
        set
        {
            _units.Clear();
            if (value == null)
            {
                return;
            }

            string[] array = value.Split(new char[2] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            string[] array2 = array;
            foreach (string text in array2)
            {
                string text2 = text.Trim();
                if (text2.Length > 0)
                {
                    _units.Add(text2);
                }
            }
        }
    }

    /// <summary>
    /// Default constructor. Initializes a new instance of the <see cref="Organization"/> class.
    /// </summary>
    public Organization() => _units = new StringCollection();

    /// <summary>
    /// Initializes a new instance of the <see cref="Organization"/> class with the specified value.
    /// </summary>
    /// <param name="value">The organization value as a string.</param>
    public Organization(string value)
    {
        _units = new StringCollection();

        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new OrganizationSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }

    /// <summary>
    /// Determines whether the current <see cref="Organization"/> object is equal to another <see cref="Organization"/> object.
    /// </summary>
    /// <param name="other">The <see cref="Organization"/> object to compare with the current object.</param>
    /// <returns>True if the current object is equal to the other object; otherwise, false.</returns>
    protected bool Equals(Organization other)
    {
        return string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase) &&
               Units.Cast<string>().SequenceEqual(other.Units.Cast<string>(), StringComparer.OrdinalIgnoreCase);
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj != null && (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Organization)obj));
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            int hash = 17;
            hash = hash * 23 + (Name != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Name) : 0);
            hash = hash * 23 + Units.Cast<string>().Aggregate(0, (current, unit) => current * 31 + StringComparer.OrdinalIgnoreCase.GetHashCode(unit));
            return hash;
        }
    }
}