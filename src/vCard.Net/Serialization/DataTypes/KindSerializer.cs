using System;
using System.IO;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for <see cref="Kind"/> values, providing methods to serialize and deserialize kind data types.
/// </summary>
public class KindSerializer : EncodableDataTypeSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="KindSerializer"/> class.
    /// </summary>
    public KindSerializer() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KindSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public KindSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(Kind);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        if (obj is not Kind kind)
        {
            return null;
        }

        string cardKind;

        switch (kind.CardKind)
        {
            case KindType.None:
                return null;
            case KindType.Other:
                cardKind = kind.OtherKind;
                break;
            case KindType.Organization:
                cardKind = "org";
                break;
            default:
                cardKind = kind.CardKind.ToString().ToLowerInvariant();
                break;
        }

        return string.IsNullOrEmpty(cardKind) ? null : Encode(kind, cardKind);
    }

    /// <summary>
    /// Deserializes a string representation of a <see cref="Kind"/>.
    /// </summary>
    /// <param name="value">The string representation of the <see cref="Kind"/>.</param>
    /// <returns>The deserialized <see cref="Kind"/>.</returns>
    public Kind Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new Kind();
        }

        if (CreateAndAssociate() is not Kind kind)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(kind, value);

        if (value != null)
        {
            string text = value.Trim().ToLowerInvariant();
            kind.OtherKind = null;
            switch (text)
            {
                case "individual":
                    kind.CardKind = KindType.Individual;
                    break;
                case "group":
                    kind.CardKind = KindType.Group;
                    break;
                case "org":
                    kind.CardKind = KindType.Organization;
                    break;
                case "location":
                    kind.CardKind = KindType.Location;
                    break;
                default:
                    kind.OtherKind = text;
                    break;
            }
        }
        else
        {
            kind.CardKind = KindType.None;
        }

        return kind;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}