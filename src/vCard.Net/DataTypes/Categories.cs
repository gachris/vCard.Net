using System.Collections.Specialized;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents the Categories (CATEGORIES) property of a vCard object.
/// </summary>
/// <remarks>
/// This property class parses the <see cref="CategoriesString"/>
/// property and allows access to the component parts. It is used to specify application
/// category information about the object. This property is valid for use with
/// vCard 3.0 and vCard 4.0 specification objects.
/// </remarks>
public class Categories : EncodableDataType
{
    private readonly StringCollection _categories;

    /// <summary>
    /// Gets the versions of the vCard specification supported by this property.
    /// </summary>
    /// <value>
    /// Supports all specifications.
    /// </value>
    public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;

    /// <summary>
    /// Gets the collection of categories.
    /// </summary>
    /// <value>
    /// Categories can be added to or removed from the returned collection reference.
    /// </value>
    public StringCollection Collection => _categories;

    /// <summary>
    /// Gets or sets the categories as a string value.
    /// </summary>
    /// <value>
    /// The string can contain one or more categories separated by commas or semi-colons.
    /// The string will be split and loaded into the categories string collection.
    /// </value>
    public virtual string CategoriesString
    {
        get => string.Join(", ", _categories);
        set
        {
            _categories.Clear();
            if (value == null)
            {
                return;
            }

            string[] array = value.Split(',', ';');
            foreach (string text in array)
            {
                string trimmedText = text.Trim();
                if (!string.IsNullOrWhiteSpace(trimmedText))
                {
                    _categories.Add(trimmedText);
                }
            }
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Categories"/> class.
    /// </summary>
    public Categories()
    {
        _categories = new StringCollection();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Categories"/> class with the specified value.
    /// </summary>
    /// <param name="value">The categories value.</param>
    public Categories(string value)
    {
        _categories = new StringCollection();

        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new CategoriesSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }

    /// <summary>
    /// Determines whether the current <see cref="Categories"/> object is equal to another <see cref="Categories"/> object.
    /// </summary>
    /// <param name="other">The <see cref="Categories"/> object to compare with the current object.</param>
    /// <returns>True if the current object is equal to the other object; otherwise, false.</returns>
    protected bool Equals(Categories other)
    {
        if (_categories.Count != other._categories.Count)
            return false;

        for (var i = 0; i < _categories.Count; i++)
        {
            if (!string.Equals(_categories[i], other._categories[i], StringComparison.OrdinalIgnoreCase))
                return false;
        }

        return true;
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj != null && (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Categories)obj));
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = 17;
            foreach (var category in _categories)
            {
                hashCode = hashCode * 23 + (category != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(category) : 0);
            }
            return hashCode;
        }
    }
}