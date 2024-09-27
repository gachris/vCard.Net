using System.Text;
using System.Text.RegularExpressions;
using vCard.Net.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializes and deserializes Categories objects.
/// </summary>
public class CategoriesSerializer : StringSerializer
{
    private static readonly Regex _reSplit = new Regex("(?:^[,;])|(?<=(?:[^\\\\]))[,;]");

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoriesSerializer"/> class.
    /// </summary>
    public CategoriesSerializer() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoriesSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public CategoriesSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(Categories);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        if (obj is not Categories categories)
        {
            return null;
        }

        if (categories.Collection.Count == 0)
        {
            return null;
        }

        var stringBuilder = new StringBuilder(256);
        foreach (string category in categories.Collection)
        {
            stringBuilder.Append(',');
            stringBuilder.Append(category.Escape());
        }

        stringBuilder.Remove(0, 1);

        return Encode(categories, stringBuilder.ToString());
    }

    /// <summary>
    /// Deserializes a Categories object from a string value.
    /// </summary>
    /// <param name="value">The string representation of the Categories object.</param>
    /// <returns>The deserialized Categories object.</returns>
    public Categories Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not Categories categories)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(categories, value);

        if (value == null || value.Length <= 0)
        {
            return null;
        }

        categories.Collection.Clear();

        string[] array = _reSplit.Split(value);
        string[] array2 = array;
        foreach (string text in array2)
        {
            string text2 = text.Trim().Unescape();
            if (text2.Length > 0)
            {
                categories.Collection.Add(text2);
            }
        }

        return categories;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}