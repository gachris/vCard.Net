using System;
using System.IO;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for <see cref="Source"/> objects.
/// </summary>
public class SourceSerializer : StringSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SourceSerializer"/> class.
    /// </summary>
    public SourceSerializer() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SourceSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public SourceSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(Source);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        if (obj is not Source source)
        {
            return null;
        }

        try
        {
            return source?.Value == null ? null : Encode(source, Escape(source.Value.OriginalString));
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Deserializes the specified value into a <see cref="Source"/> object.
    /// </summary>
    /// <param name="value">The string value to deserialize.</param>
    /// <returns>The deserialized <see cref="Source"/> object.</returns>
    public Source Deserialize(string value)
    {
        Source source = null;
        try
        {
            source = CreateAndAssociate() as Source;
            if (source != null)
            {
                var uriString = Unescape(Decode(source, value));
                source.Value = new Uri(uriString);
            }
        }
        catch { }

        return source;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}