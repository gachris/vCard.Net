using System.Collections.Specialized;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents the expertise (EXPERTISE) property of a vCard object.
/// </summary>
/// <remarks>
/// This property class parses the <see cref="Value"/>
/// property and allows access to the component parts. It is used to specify application
/// category information about the object. This property is valid for use with
/// vCard 4.0 specification objects.
/// </remarks>
public class Expertise : EncodableDataType
{
    private readonly StringCollection _collection;

    /// <summary>
    /// Gets the collection of expertise.
    /// </summary>
    /// <value>
    /// Expertise can be added to or removed from the returned collection reference.
    /// </value>
    public StringCollection Collection => _collection;

    /// <summary>
    /// Gets or sets the expertise as a string value.
    /// </summary>
    /// <value>
    /// The string can contain one or more expertise separated by commas or semi-colons.
    /// The string will be split and loaded into the expertise string collection.
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
    /// Initializes a new instance of the <see cref="Expertise"/> class.
    /// </summary>
    public Expertise()
    {
        _collection = new StringCollection();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Expertise"/> class with the specified value.
    /// </summary>
    /// <param name="value">The expertise value.</param>
    public Expertise(string value)
    {
        _collection = new StringCollection();

        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new ExpertiseSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }

    /// <summary>
    /// Determines whether the current <see cref="Expertise"/> object is equal to another <see cref="Expertise"/> object.
    /// </summary>
    /// <param name="other">The <see cref="Expertise"/> object to compare with the current object.</param>
    /// <returns>True if the current object is equal to the other object; otherwise, false.</returns>
    protected bool Equals(Expertise other)
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
        return obj != null && (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Expertise)obj));
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