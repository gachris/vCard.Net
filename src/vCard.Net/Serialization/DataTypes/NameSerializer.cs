﻿using System.Text.RegularExpressions;
using vCard.Net.CardComponents;
using vCard.Net.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for <see cref="StructuredName"/> objects.
/// </summary>
public class NameSerializer : StringSerializer
{
    private static readonly Regex _reSplit = new Regex("(?:^[;])|(?<=(?:[^\\\\]))[;]");

    /// <summary>
    /// Initializes a new instance of the <see cref="NameSerializer"/> class.
    /// </summary>
    public NameSerializer() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NameSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public NameSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(StructuredName);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        if (obj is not StructuredName name)
        {
            return null;
        }

        var version = VCardVersion.vCard2_1;
        if (SerializationContext.Peek() is IVCardProperty property && property.Parent is IVCardComponent component)
        {
            version = component.Version;
        }

        var array = new string[5];

        if (name.FamilyName != null && name.FamilyName.Length > 0)
        {
            array[0] = version == VCardVersion.vCard2_1 ? name.FamilyName.RestrictedEscape() : name.FamilyName.Escape();
        }

        if (name.GivenName != null && name.GivenName.Length > 0)
        {
            array[1] = version == VCardVersion.vCard2_1 ? name.GivenName.RestrictedEscape() : name.GivenName.Escape();
        }

        if (name.AdditionalNames != null && name.AdditionalNames.Length > 0)
        {
            array[2] = version == VCardVersion.vCard2_1
                ? name.AdditionalNames.RestrictedEscape()
                : name.AdditionalNames.Escape();
        }

        if (name.NamePrefix != null && name.NamePrefix.Length > 0)
        {
            array[3] = version == VCardVersion.vCard2_1 ? name.NamePrefix.RestrictedEscape() : name.NamePrefix.Escape();
        }

        if (name.NameSuffix != null && name.NameSuffix.Length > 0)
        {
            array[4] = version == VCardVersion.vCard2_1 ? name.NameSuffix.RestrictedEscape() : name.NameSuffix.Escape();
        }

        return Encode(name, string.Join(";", array));
    }

    /// <summary>
    /// Deserializes a string representation of a <see cref="StructuredName"/>.
    /// </summary>
    /// <param name="value">The string representation of the <see cref="StructuredName"/>.</param>
    /// <returns>The deserialized <see cref="StructuredName"/>.</returns>
    public StructuredName Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not StructuredName name)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(name, value);

        if (value is null)
        {
            return null;
        }

        if (value.Length > 0)
        {
            var array = _reSplit.Split(value);

            if (array.Length != 0)
            {
                name.FamilyName = array[0].Unescape();
            }

            if (array.Length > 1)
            {
                name.GivenName = array[1].Unescape();
            }

            if (array.Length > 2)
            {
                name.AdditionalNames = array[2].Unescape();
            }

            if (array.Length > 3)
            {
                name.NamePrefix = array[3].Unescape();
            }

            if (array.Length > 4)
            {
                name.NameSuffix = array[4].Unescape();
            }
        }

        return name;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}