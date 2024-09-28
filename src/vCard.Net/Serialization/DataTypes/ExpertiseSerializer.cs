using System.Text;
using System.Text.RegularExpressions;
using vCard.Net.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializes and deserializes Expertise objects.
/// </summary>
public class ExpertiseSerializer : StringSerializer
{
    private static readonly Regex _reSplit = new Regex("(?:^[,;])|(?<=(?:[^\\\\]))[,;]");

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpertiseSerializer"/> class.
    /// </summary>
    public ExpertiseSerializer() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpertiseSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public ExpertiseSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(Expertise);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        if (obj is not Expertise expertise)
        {
            return null;
        }

        if (expertise.Collection.Count == 0)
        {
            return null;
        }

        var stringBuilder = new StringBuilder(256);
        foreach (string category in expertise.Collection)
        {
            stringBuilder.Append(',');
            stringBuilder.Append(category.Escape());
        }

        stringBuilder.Remove(0, 1);

        return Encode(expertise, stringBuilder.ToString());
    }

    /// <summary>
    /// Deserializes a Expertise object from a string value.
    /// </summary>
    /// <param name="value">The string representation of the Expertise object.</param>
    /// <returns>The deserialized Expertise object.</returns>
    public Expertise Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not Expertise expertise)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(expertise, value);

        if (value == null || value.Length <= 0)
        {
            return null;
        }

        expertise.Collection.Clear();

        string[] array = _reSplit.Split(value);
        string[] array2 = array;
        foreach (string text in array2)
        {
            string text2 = text.Trim().Unescape();
            if (text2.Length > 0)
            {
                expertise.Collection.Add(text2);
            }
        }

        return expertise;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}