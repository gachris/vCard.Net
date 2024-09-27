using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for vCard email address data types.
/// </summary>
public class EmailSerializer : EncodableDataTypeSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailSerializer"/> class.
    /// </summary>
    public EmailSerializer() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public EmailSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(Email);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        return obj is not Email email ? null : Encode(email, email.Value);
    }

    /// <inheritdoc/>
    public Email Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not Email email)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(email, value);

        if (value is null)
        {
            return null;
        }

        email.Value = value;

        return email;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}