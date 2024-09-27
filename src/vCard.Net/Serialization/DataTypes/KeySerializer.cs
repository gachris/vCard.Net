using System;
using System.IO;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for <see cref="Key"/> values, providing methods to serialize and deserialize key data types.
/// </summary>
public class KeySerializer : EncodableDataTypeSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KeySerializer"/> class.
    /// </summary>
    public KeySerializer() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KeySerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public KeySerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(Key);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        return obj is not Key key ? null : Encode(key, key.Value);
    }

    /// <summary>
    /// Deserializes a string representation of a <see cref="Key"/>.
    /// </summary>
    /// <param name="value">The string representation of the <see cref="Key"/>.</param>
    /// <returns>The deserialized <see cref="Key"/>.</returns>
    public Key Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not Key key)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(key, value);

        if (value is null)
        {
            return null;
        }

        key.Value = value;

        return key;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}