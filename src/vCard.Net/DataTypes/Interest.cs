﻿using System.Collections.Specialized;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents the interest (INTEREST) property of a vCard object.
/// </summary>
/// <remarks>
/// This property class parses the <see cref="Value"/>
/// property and allows access to the component parts. It is used to specify application
/// category information about the object. This property is valid for use with
/// vCard 4.0 specification objects.
/// </remarks>
public class Interest : EncodableDataType
{
    private readonly StringCollection _collection;

    /// <summary>
    /// Gets the collection of interest.
    /// </summary>
    /// <value>
    /// Interest can be added to or removed from the returned collection reference.
    /// </value>
    public StringCollection Collection => _collection;

    /// <summary>
    /// Gets or sets the interest as a string value.
    /// </summary>
    /// <value>
    /// The string can contain one or more interest separated by commas or semi-colons.
    /// The string will be split and loaded into the interest string collection.
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
    /// Initializes a new instance of the <see cref="Interest"/> class.
    /// </summary>
    public Interest()
    {
        _collection = new StringCollection();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Interest"/> class with the specified value.
    /// </summary>
    /// <param name="value">The interest value.</param>
    public Interest(string value)
    {
        _collection = new StringCollection();

        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new InterestSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }

    /// <summary>
    /// Determines whether the current <see cref="Interest"/> object is equal to another <see cref="Interest"/> object.
    /// </summary>
    /// <param name="other">The <see cref="Interest"/> object to compare with the current object.</param>
    /// <returns>True if the current object is equal to the other object; otherwise, false.</returns>
    protected bool Equals(Interest other)
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
        return obj != null && (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Interest)obj));
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