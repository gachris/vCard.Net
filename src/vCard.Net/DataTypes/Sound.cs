using System;
using System.IO;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents the Sound (SOUND) property of a vCard, indicating an associated sound clip or recording.
/// </summary>
public class Sound : EncodableDataType
{
    /// <summary>
    /// Gets the versions of the vCard specification supported by this property.
    /// </summary>  
    /// <value>
    /// Supports all specifications.
    /// </value>
    public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;

    /// <summary>
    /// Gets or sets the value of the sound.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Sound"/> class.
    /// </summary>
    public Sound()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Sound"/> class with the specified value.
    /// </summary>
    /// <param name="value">The value representing the sound.</param>
    public Sound(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new SoundSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }

    /// <summary>
    /// Determines whether the current <see cref="Sound"/> object is equal to another <see cref="Sound"/> object.
    /// </summary>
    /// <param name="other">The <see cref="Sound"/> object to compare with the current object.</param>
    /// <returns>True if the current object is equal to the other object; otherwise, false.</returns>
    protected bool Equals(Sound other)
    {
        return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj != null && (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Sound)obj));
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