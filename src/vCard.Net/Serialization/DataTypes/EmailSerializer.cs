using vCard.Net.CardComponents;
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
        if (obj is not Email email)
        {
            return null;
        }

        var property = SerializationContext.Peek() as IVCardProperty;
        var vCardVersion = property.Parent is IVCardComponent component ? component.Version : VCardVersion.vCard2_1;

        if (vCardVersion is VCardVersion.vCard2_1)
        {
            var types = email.Types.ToList();

            email.Parameters.Remove("TYPE");

            if (types.Any())
            {
                email.Parameters.Add("", string.Join(";", types));
            }
        }

        return Encode(email, email.Value);
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