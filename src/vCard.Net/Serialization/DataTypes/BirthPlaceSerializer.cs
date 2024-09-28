using System.Text;
using System.Text.RegularExpressions;
using vCard.Net.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializes and deserializes BirthPlace objects.
/// </summary>
public class BirthPlaceSerializer : StringSerializer
{
    private static readonly Regex _reSplit = new Regex("(?:^[,;])|(?<=(?:[^\\\\]))[,;]");

    /// <summary>
    /// Initializes a new instance of the <see cref="BirthPlaceSerializer"/> class.
    /// </summary>
    public BirthPlaceSerializer() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BirthPlaceSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public BirthPlaceSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(BirthPlace);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        if (obj is not BirthPlace birthPlace)
        {
            return null;
        }

        if (birthPlace.Collection.Count == 0)
        {
            return null;
        }

        var stringBuilder = new StringBuilder(256);
        foreach (string category in birthPlace.Collection)
        {
            stringBuilder.Append(',');
            stringBuilder.Append(category.Escape());
        }

        stringBuilder.Remove(0, 1);

        return Encode(birthPlace, stringBuilder.ToString());
    }

    /// <summary>
    /// Deserializes a BirthPlace object from a string value.
    /// </summary>
    /// <param name="value">The string representation of the BirthPlace object.</param>
    /// <returns>The deserialized BirthPlace object.</returns>
    public BirthPlace Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not BirthPlace birthPlace)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(birthPlace, value);

        if (value == null || value.Length <= 0)
        {
            return null;
        }

        birthPlace.Collection.Clear();

        string[] array = _reSplit.Split(value);
        string[] array2 = array;
        foreach (string text in array2)
        {
            string text2 = text.Trim().Unescape();
            if (text2.Length > 0)
            {
                birthPlace.Collection.Add(text2);
            }
        }

        return birthPlace;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}