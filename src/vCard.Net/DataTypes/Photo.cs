using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents the Photo (PHOTO) property of a vCard.
/// </summary>
/// <remarks>
/// This property class parses the <see cref="Value"/> property
/// and allows access to the component organization name and unit parts.
/// </remarks>
public class Photo : EncodableDataType
{
    /// <summary>
    /// Gets or sets the value of the photo.
    /// </summary>
    public virtual string Value { get; set; }

    /// <summary>
    /// Gets or sets the type of the photo.
    /// </summary>
    public virtual string Type
    {
        get => Parameters.Get("TYPE");
        set => Parameters.Set("TYPE", value);
    }

    /// <summary>
    /// Gets or sets the media type of the photo.
    /// </summary>
    public virtual string MediaType
    {
        get => Parameters.Get("MEDIATYPE");
        set => Parameters.Set("MEDIATYPE", value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Photo"/> class.
    /// </summary>
    public Photo()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Photo"/> class with the specified value.
    /// </summary>
    /// <param name="value">The photo value.</param>
    public Photo(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new PhotoSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }

    /// <summary>
    /// Determines whether the current <see cref="Photo"/> object is equal to another <see cref="Photo"/> object.
    /// </summary>
    /// <param name="other">The <see cref="Photo"/> object to compare with the current object.</param>
    /// <returns>True if the current object is equal to the other object; otherwise, false.</returns>
    protected bool Equals(Photo other)
    {
        return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj != null && (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Photo)obj));
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            var hashCode = 17;
            hashCode = hashCode * 23 + (Value != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Value) : 0);
            return hashCode;
        }
    }
}