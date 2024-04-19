using System.IO;
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
public class Name : EncodableDataType
{
    /// <summary>
    /// Gets or sets the family name (last name).
    /// </summary>
    public string FamilyName { get; set; }

    /// <summary>
    /// Gets or sets the given name (first name).
    /// </summary>
    public string GivenName { get; set; }

    /// <summary>
    /// Gets or sets additional names such as one or more middle names.
    /// </summary>
    public string AdditionalNames { get; set; }

    /// <summary>
    /// Gets or sets the name prefix (i.e., Mr., Mrs.).
    /// </summary>
    public string NamePrefix { get; set; }

    /// <summary>
    /// Gets or sets the name suffix (i.e., Jr, Sr).
    /// </summary>
    public string NameSuffix { get; set; }

    /// <summary>
    /// Gets or sets the string to use when sorting names.
    /// </summary>
    /// <value>
    /// If set, this value should be given precedence over the <see cref="SortableName"/> property.
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
    /// Gets the name in a format suitable for sorting by last name.
    /// </summary>
    /// <remarks>
    /// The name is returned in a comma-separated format in the order family name, given name, additional names, name suffix, name prefix.
    /// </remarks>
    public string SortableName
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
    public string FormattedName
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
    /// Initializes a new instance of the <see cref="Name"/> class.
    /// </summary>
    public Name()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Name"/> class with the specified value.
    /// </summary>
    /// <param name="value">The name value.</param>
    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new NameSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }
}