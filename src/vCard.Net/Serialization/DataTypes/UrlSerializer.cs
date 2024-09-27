using System;
using System.IO;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for URL data type, providing serialization and deserialization methods.
/// </summary>
public class UrlSerializer : StringSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UrlSerializer"/> class.
    /// </summary>
    public UrlSerializer() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UrlSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public UrlSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(Url);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        return obj is not Url url ? null : Encode(url, url.Value);
    }

    /// <summary>
    /// Deserializes the URL from its string representation.
    /// </summary>
    /// <param name="value">The string representation of the URL.</param>
    /// <returns>The deserialized URL.</returns>
    public Url Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not Url url)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(url, value);

        if (value is null)
        {
            return null;
        }

        url.Value = value;

        return url;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}
