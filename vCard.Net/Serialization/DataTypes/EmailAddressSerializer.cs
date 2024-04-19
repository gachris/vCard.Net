using System;
using System.IO;
using vCard.Net.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for vCard email address data types.
/// </summary>
public class EmailAddressSerializer : EncodableDataTypeSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAddressSerializer"/> class.
    /// </summary>
    public EmailAddressSerializer() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAddressSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public EmailAddressSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(EmailAddress);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        return obj is not EmailAddress emailAddress ? null : Encode(emailAddress, emailAddress.Value);
    }

    /// <inheritdoc/>
    public EmailAddress Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not EmailAddress emailAddress)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(emailAddress, value);

        if (value is null)
        {
            return null;
        }

        emailAddress.Value = value;

        return emailAddress;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}