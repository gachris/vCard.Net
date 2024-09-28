using System.Collections.Specialized;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents the death place (DEATHPLACE) property of a vCard object.
/// </summary>
/// <remarks>
/// This property class parses the <see cref="Value"/>
/// property and allows access to the component parts. It is used to specify application
/// category information about the object. This property is valid for use with
/// vCard 4.0 specification objects.
/// </remarks>
public class DeathPlace : EncodableDataType
{
    private readonly StringCollection _collection;

    /// <summary>
    /// Gets the collection of death place.
    /// </summary>
    /// <value>
    /// DeathPlace can be added to or removed from the returned collection reference.
    /// </value>
    public StringCollection Collection => _collection;

    /// <summary>
    /// Gets or sets the death place as a string value.
    /// </summary>
    /// <value>
    /// The string can contain one or more death place separated by commas or semi-colons.
    /// The string will be split and loaded into the death place string collection.
    /// </value>
    public virtual string Value
    {
        get => string.Join(", ", _collection);
        set
        {
            _collection.Clear();
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
                    _collection.Add(trimmedText);
                }
            }
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeathPlace"/> class.
    /// </summary>
    public DeathPlace()
    {
        _collection = new StringCollection();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeathPlace"/> class with the specified value.
    /// </summary>
    /// <param name="value">The death place value.</param>
    public DeathPlace(string value)
    {
        _collection = new StringCollection();

        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new DeathPlaceSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }

    /// <summary>
    /// Determines whether the current <see cref="DeathPlace"/> object is equal to another <see cref="DeathPlace"/> object.
    /// </summary>
    /// <param name="other">The <see cref="DeathPlace"/> object to compare with the current object.</param>
    /// <returns>True if the current object is equal to the other object; otherwise, false.</returns>
    protected bool Equals(DeathPlace other)
    {
        if (_collection.Count != other._collection.Count)
            return false;

        for (var i = 0; i < _collection.Count; i++)
        {
            if (!string.Equals(_collection[i], other._collection[i], StringComparison.OrdinalIgnoreCase))
                return false;
        }

        return true;
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj != null && (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((DeathPlace)obj));
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = 17;
            foreach (var category in _collection)
            {
                hashCode = hashCode * 23 + (category != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(category) : 0);
            }
            return hashCode;
        }
    }
}