using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for <see cref="Language"/> objects.
/// </summary>
public class LanguageSerializer : EncodableDataTypeSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LanguageSerializer"/> class.
    /// </summary>
    public LanguageSerializer() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LanguageSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public LanguageSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(Language);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        return obj is not Language language ? null : Encode(language, language.Value);
    }

    /// <summary>
    /// Deserializes the specified value into a <see cref="Language"/> object.
    /// </summary>
    /// <param name="value">The string value to deserialize.</param>
    /// <returns>The deserialized <see cref="Language"/> object.</returns>
    public Language Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not Language language)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(language, value);

        if (value is null)
        {
            return null;
        }

        language.Value = value;

        return language;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}
