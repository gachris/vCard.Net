using System.Text;
using System.Text.RegularExpressions;
using vCard.Net.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializes and deserializes DeathPlace objects.
/// </summary>
public class DeathPlaceSerializer : StringSerializer
{
    private static readonly Regex _reSplit = new Regex("(?:^[,;])|(?<=(?:[^\\\\]))[,;]");

    /// <summary>
    /// Initializes a new instance of the <see cref="DeathPlaceSerializer"/> class.
    /// </summary>
    public DeathPlaceSerializer() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeathPlaceSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public DeathPlaceSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(DeathPlace);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        if (obj is not DeathPlace deathPlace)
        {
            return null;
        }

        if (deathPlace.Collection.Count == 0)
        {
            return null;
        }

        var stringBuilder = new StringBuilder(256);
        foreach (string category in deathPlace.Collection)
        {
            stringBuilder.Append(',');
            stringBuilder.Append(category.Escape());
        }

        stringBuilder.Remove(0, 1);

        return Encode(deathPlace, stringBuilder.ToString());
    }

    /// <summary>
    /// Deserializes a DeathPlace object from a string value.
    /// </summary>
    /// <param name="value">The string representation of the DeathPlace object.</param>
    /// <returns>The deserialized DeathPlace object.</returns>
    public DeathPlace Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not DeathPlace deathPlace)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(deathPlace, value);

        if (value == null || value.Length <= 0)
        {
            return null;
        }

        deathPlace.Collection.Clear();

        string[] array = _reSplit.Split(value);
        string[] array2 = array;
        foreach (string text in array2)
        {
            string text2 = text.Trim().Unescape();
            if (text2.Length > 0)
            {
                deathPlace.Collection.Add(text2);
            }
        }

        return deathPlace;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}