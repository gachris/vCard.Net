using System.Text;
using System.Text.RegularExpressions;
using vCard.Net.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializes and deserializes Interest objects.
/// </summary>
public class InterestSerializer : StringSerializer
{
    private static readonly Regex _reSplit = new Regex("(?:^[,;])|(?<=(?:[^\\\\]))[,;]");

    /// <summary>
    /// Initializes a new instance of the <see cref="InterestSerializer"/> class.
    /// </summary>
    public InterestSerializer() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InterestSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public InterestSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(Interest);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        if (obj is not Interest interest)
        {
            return null;
        }

        if (interest.Collection.Count == 0)
        {
            return null;
        }

        var stringBuilder = new StringBuilder(256);
        foreach (string category in interest.Collection)
        {
            stringBuilder.Append(',');
            stringBuilder.Append(category.Escape());
        }

        stringBuilder.Remove(0, 1);

        return Encode(interest, stringBuilder.ToString());
    }

    /// <summary>
    /// Deserializes a Interest object from a string value.
    /// </summary>
    /// <param name="value">The string representation of the Interest object.</param>
    /// <returns>The deserialized Interest object.</returns>
    public Interest Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not Interest interest)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(interest, value);

        if (value == null || value.Length <= 0)
        {
            return null;
        }

        interest.Collection.Clear();

        string[] array = _reSplit.Split(value);
        string[] array2 = array;
        foreach (string text in array2)
        {
            string text2 = text.Trim().Unescape();
            if (text2.Length > 0)
            {
                interest.Collection.Add(text2);
            }
        }

        return interest;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}