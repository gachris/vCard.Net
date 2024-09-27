using System.Globalization;
using vCard.Net.CardComponents;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for <see cref="GeographicPosition"/> objects, providing methods to serialize and deserialize geographic position information.
/// </summary>
public class GeographicPositionSerializer : StringSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GeographicPositionSerializer"/> class.
    /// </summary>
    public GeographicPositionSerializer() : base() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="GeographicPositionSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public GeographicPositionSerializer(SerializationContext ctx) : base(ctx) { }

    /// <inheritdoc/>
    public override Type TargetType => typeof(GeographicPosition);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        if (obj is not GeographicPosition geographicPosition)
        {
            return null;
        }

        if (geographicPosition.Latitude == 0.0 && geographicPosition.Longitude == 0.0)
        {
            return null;
        }

        var version = vCardVersion.vCard2_1;
        if (SerializationContext.Peek() is IvCardProperty property && property.Parent is IvCardComponent component)
        {
            version = component.Version;
        }

        string text = string.Empty;
        if (version == vCardVersion.vCard4_0 && geographicPosition.IncludeGeoUriPrefix)
        {
            text = "geo:";
        }

        var value = string.Format(CultureInfo.InvariantCulture, "{0}{1:F6}{2}{3:F6}", text, geographicPosition.Latitude, (version is vCardVersion.vCard3_0 or vCardVersion.vCard4_0) ? ";" : ",", geographicPosition.Longitude);

        return Encode(geographicPosition, value);
    }

    /// <summary>
    /// Deserializes the string value into a <see cref="GeographicPosition"/> object.
    /// </summary>
    /// <param name="value">The string value to deserialize.</param>
    /// <returns>The deserialized <see cref="GeographicPosition"/> object.</returns>
    public GeographicPosition Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not GeographicPosition geographicPosition)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(geographicPosition, value);

        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        double num3 = (geographicPosition.Latitude = (geographicPosition.Longitude = 0.0));

        if (value.StartsWith("geo:", StringComparison.OrdinalIgnoreCase))
        {
            geographicPosition.IncludeGeoUriPrefix = true;
            value = value.Substring(4);
        }
        else
        {
            geographicPosition.IncludeGeoUriPrefix = false;
        }

        string[] array = value.Split(new char[2] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
        if (array.Length == 2)
        {
            geographicPosition.Latitude = Convert.ToDouble(array[0], CultureInfo.InvariantCulture);
            geographicPosition.Longitude = Convert.ToDouble(array[1], CultureInfo.InvariantCulture);
        }

        return geographicPosition;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}