using System;
using System.IO;
using System.Linq;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for <see cref="Gender"/> objects, providing methods to serialize and deserialize gender information.
/// </summary>
public class GenderSerializer : StringSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GenderSerializer"/> class.
    /// </summary>
    public GenderSerializer() : base() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="GenderSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public GenderSerializer(SerializationContext ctx) : base(ctx) { }

    /// <inheritdoc/>
    public override Type TargetType => typeof(Gender);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        if (obj is not Gender gender)
        {
            return null;
        }

        if (!gender.Sex.HasValue && string.IsNullOrWhiteSpace(gender.GenderIdentity))
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(gender.GenderIdentity))
        {
            return gender.Sex.ToString();
        }

        return Encode(gender, string.Join(";", gender.Sex?.ToString(), gender.GenderIdentity));
    }

    /// <summary>
    /// Deserializes the string value into a <see cref="Gender"/> object.
    /// </summary>
    /// <param name="value">The string value to deserialize.</param>
    /// <returns>The deserialized <see cref="Gender"/> object.</returns>
    public Gender Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not Gender gender)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(gender, value);

        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        gender.Sex = null;
        gender.GenderIdentity = null;
        string[] array = value.Split(new char[1] { ';' });
        if (array[0].Length != 0)
        {
            gender.Sex = array[0][0];
            if (!new char[5] { 'M', 'F', 'O', 'N', 'U' }.Contains(gender.Sex.Value))
            {
                gender.Sex = null;
            }
        }

        if (array.Length > 1)
        {
            gender.GenderIdentity = array[1];
        }

        return gender;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}