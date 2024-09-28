using System.Text;
using System.Text.RegularExpressions;
using vCard.Net.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializes and deserializes Hobby objects.
/// </summary>
public class HobbySerializer : StringSerializer
{
    private static readonly Regex _reSplit = new Regex("(?:^[,;])|(?<=(?:[^\\\\]))[,;]");

    /// <summary>
    /// Initializes a new instance of the <see cref="HobbySerializer"/> class.
    /// </summary>
    public HobbySerializer() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HobbySerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public HobbySerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(Hobby);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        if (obj is not Hobby hobby)
        {
            return null;
        }

        if (hobby.Collection.Count == 0)
        {
            return null;
        }

        var stringBuilder = new StringBuilder(256);
        foreach (string category in hobby.Collection)
        {
            stringBuilder.Append(',');
            stringBuilder.Append(category.Escape());
        }

        stringBuilder.Remove(0, 1);

        return Encode(hobby, stringBuilder.ToString());
    }

    /// <summary>
    /// Deserializes a Hobby object from a string value.
    /// </summary>
    /// <param name="value">The string representation of the Hobby object.</param>
    /// <returns>The deserialized Hobby object.</returns>
    public Hobby Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not Hobby hobby)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(hobby, value);

        if (value == null || value.Length <= 0)
        {
            return null;
        }

        hobby.Collection.Clear();

        string[] array = _reSplit.Split(value);
        string[] array2 = array;
        foreach (string text in array2)
        {
            string text2 = text.Trim().Unescape();
            if (text2.Length > 0)
            {
                hobby.Collection.Add(text2);
            }
        }

        return hobby;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}