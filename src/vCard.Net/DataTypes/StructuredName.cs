using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents the Name (N) property of a vCard.
/// </summary>
/// <remarks>
/// This property class parses the <see cref="SortableName"/> property
/// and allows access to the component name parts. It is based on the semantics of
/// the X.520 individual name attributes.
/// </remarks>
public class StructuredName : EncodableDataType
{
    /// <summary>
    /// Gets the versions of the vCard specification supported by this property.
    /// </summary>  
    /// <value>
    /// Supports all specifications.
    /// </value>
    public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;

    /// <summary>
    /// Gets or sets the family name (last name).
    /// </summary>
    public virtual string FamilyName { get; set; }

    /// <summary>
    /// Gets or sets the given name (first name).
    /// </summary>
    public virtual string GivenName { get; set; }

    /// <summary>
    /// Gets or sets additional names such as one or more middle names.
    /// </summary>
    public virtual string AdditionalNames { get; set; }

    /// <summary>
    /// Gets or sets the name prefix (i.e., Mr., Mrs.).
    /// </summary>
    public virtual string NamePrefix { get; set; }

    /// <summary>
    /// Gets or sets the name suffix (i.e., Jr, Sr).
    /// </summary>
    public virtual string NameSuffix { get; set; }

    /// <summary>
    /// Gets or sets the string to use when sorting names.
    /// </summary>
    /// <value>
    /// If set, this value should be given precedence over the <see cref="SortableName"/> property.
    /// </value>
    public virtual string SortAs
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
    /// Gets the name in a format suitable for sorting by last name.
    /// </summary>
    /// <remarks>
    /// The name is returned in a comma-separated format in the order family name, given name, additional names, name suffix, name prefix.
    /// </remarks>
    public virtual string SortableName
    {
        get
        {
            string[] array = new string[5];
            int num = 0;
            if (!string.IsNullOrWhiteSpace(FamilyName))
            {
                array[num++] = FamilyName;
            }

            if (!string.IsNullOrWhiteSpace(GivenName))
            {
                array[num++] = GivenName;
            }

            if (!string.IsNullOrWhiteSpace(AdditionalNames))
            {
                array[num++] = AdditionalNames;
            }

            if (!string.IsNullOrWhiteSpace(NameSuffix))
            {
                array[num++] = NameSuffix;
            }

            if (!string.IsNullOrWhiteSpace(NamePrefix))
            {
                array[num++] = NamePrefix;
            }

            return num == 0 ? "Unknown" : string.Join(", ", array, 0, num);
        }
    }

    /// <summary>
    /// Gets the full, formatted name.
    /// </summary>
    public virtual string FormattedName
    {
        get
        {
            string[] array = new string[5];
            int num = 0;
            if (!string.IsNullOrWhiteSpace(NamePrefix))
            {
                array[num++] = NamePrefix;
            }

            if (!string.IsNullOrWhiteSpace(GivenName))
            {
                array[num++] = GivenName;
            }

            if (!string.IsNullOrWhiteSpace(AdditionalNames))
            {
                array[num++] = AdditionalNames;
            }

            if (!string.IsNullOrWhiteSpace(FamilyName))
            {
                array[num++] = FamilyName;
            }

            if (!string.IsNullOrWhiteSpace(NameSuffix))
            {
                array[num++] = NameSuffix;
            }

            return num == 0 ? "Unknown" : string.Join(" ", array, 0, num);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StructuredName"/> class.
    /// </summary>
    public StructuredName()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StructuredName"/> class with the specified value.
    /// </summary>
    /// <param name="value">The name value.</param>
    public StructuredName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new NameSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }

    /// <summary>
    /// Determines whether the current <see cref="StructuredName"/> object is equal to another <see cref="StructuredName"/> object.
    /// </summary>
    /// <param name="other">The <see cref="StructuredName"/> object to compare with the current object.</param>
    /// <returns>True if the current object is equal to the other object; otherwise, false.</returns>
    protected bool Equals(StructuredName other)
    {
        return string.Equals(FamilyName, other.FamilyName, StringComparison.OrdinalIgnoreCase)
               && string.Equals(GivenName, other.GivenName, StringComparison.OrdinalIgnoreCase)
               && string.Equals(AdditionalNames, other.AdditionalNames, StringComparison.OrdinalIgnoreCase)
               && string.Equals(NamePrefix, other.NamePrefix, StringComparison.OrdinalIgnoreCase)
               && string.Equals(NameSuffix, other.NameSuffix, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj != null && (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((StructuredName)obj));
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            var hashCode = 17;
            hashCode = hashCode * 23 + (FamilyName != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(FamilyName) : 0);
            hashCode = hashCode * 23 + (GivenName != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(GivenName) : 0);
            hashCode = hashCode * 23 + (AdditionalNames != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(AdditionalNames) : 0);
            hashCode = hashCode * 23 + (NamePrefix != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(NamePrefix) : 0);
            hashCode = hashCode * 23 + (NameSuffix != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(NameSuffix) : 0);
            return hashCode;
        }
    }
}