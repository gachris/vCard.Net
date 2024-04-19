using System;
using System.IO;
using System.Text.RegularExpressions;
using vCard.Net.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for <see cref="Name"/> objects.
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
    public override Type TargetType => typeof(Name);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        if (obj is not Name name)
        {
            return null;
        }

        vCardVersion specificationVersions = name.Version;
        string[] array = new string[5];
        int num = 0;
        if (!string.IsNullOrWhiteSpace(name.FamilyName))
        {
            num = 1;
            array[0] = specificationVersions == vCardVersion.vCard21 ? name.FamilyName.RestrictedEscape() : name.FamilyName.Escape();
        }

        if (!string.IsNullOrWhiteSpace(name.GivenName))
        {
            num = 2;
            array[1] = specificationVersions == vCardVersion.vCard21 ? name.GivenName.RestrictedEscape() : name.GivenName.Escape();
        }

        if (!string.IsNullOrWhiteSpace(name.AdditionalNames))
        {
            num = 3;
            array[2] = specificationVersions == vCardVersion.vCard21
                ? name.AdditionalNames.RestrictedEscape()
                : name.AdditionalNames.Escape();
        }

        if (!string.IsNullOrWhiteSpace(name.NamePrefix))
        {
            num = 4;
            array[3] = specificationVersions == vCardVersion.vCard21 ? name.NamePrefix.RestrictedEscape() : name.NamePrefix.Escape();
        }

        if (!string.IsNullOrWhiteSpace(name.NameSuffix))
        {
            num = 5;
            array[4] = specificationVersions == vCardVersion.vCard21 ? name.NameSuffix.RestrictedEscape() : name.NameSuffix.Escape();
        }

        return num == 0 ? Encode(name, "Unknown") : Encode(name, string.Join(";", array, 0, num));
    }

    /// <summary>
    /// Deserializes a string representation of a <see cref="Name"/>.
    /// </summary>
    /// <param name="value">The string representation of the <see cref="Name"/>.</param>
    /// <returns>The deserialized <see cref="Name"/>.</returns>
    public Name Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not Name name)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(name, value);

        if (value is null)
        {
            return null;
        }

        string text2 = (name.NameSuffix = null);
        string text4 = (name.NamePrefix = text2);
        string text6 = (name.AdditionalNames = text4);
        string text9 = (name.FamilyName = (name.GivenName = text6));
        if (value.Length > 0)
        {
            string[] array = _reSplit.Split(value);
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