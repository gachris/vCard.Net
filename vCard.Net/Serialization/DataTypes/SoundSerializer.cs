using System;
using System.IO;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for <see cref="Sound"/> objects.
/// </summary>
public class SoundSerializer : EncodableDataTypeSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SoundSerializer"/> class.
    /// </summary>
    public SoundSerializer() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SoundSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public SoundSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(Sound);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        return obj is not Sound sound ? null : Encode(sound, sound.Value);
    }

    /// <summary>
    /// Deserializes the specified value into a <see cref="Sound"/> object.
    /// </summary>
    /// <param name="value">The string value to deserialize.</param>
    /// <returns>The deserialized <see cref="Sound"/> object.</returns>
    public Sound Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not Sound sound)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(sound, value);

        if (value is null)
        {
            return null;
        }

        sound.Value = value;

        return sound;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}