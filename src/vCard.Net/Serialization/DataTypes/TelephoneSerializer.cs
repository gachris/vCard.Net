﻿using vCard.Net.CardComponents;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for <see cref="Telephone"/> objects.
/// </summary>
public class TelephoneSerializer : EncodableDataTypeSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TelephoneSerializer"/> class.
    /// </summary>
    public TelephoneSerializer()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TelephoneSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public TelephoneSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(Telephone);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        if (obj is not Telephone telephone)
        {
            return null;
        }

        var property = SerializationContext.Peek() as IVCardProperty;
        var vCardVersion = property.Parent is IVCardComponent component ? component.Version : VCardVersion.vCard2_1;

        if (vCardVersion is VCardVersion.vCard2_1)
        {
            var types = telephone.Types.ToList();

            telephone.Parameters.Remove("TYPE");

            if (types.Any())
            {
                telephone.Parameters.Add("", string.Join(";", types));
            }
        }

        return Encode(telephone, telephone.Value);
    }

    /// <summary>
    /// Deserializes the specified value into a <see cref="Telephone"/> object.
    /// </summary>
    /// <param name="value">The string value to deserialize.</param>
    /// <returns>The deserialized <see cref="Telephone"/> object.</returns>
    public Telephone Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not Telephone telephone)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(telephone, value);

        if (value is null)
        {
            return null;
        }

        telephone.Value = value;

        return telephone;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}