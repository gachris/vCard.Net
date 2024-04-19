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
}