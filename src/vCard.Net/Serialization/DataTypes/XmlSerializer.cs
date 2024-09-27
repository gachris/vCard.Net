using System;
using System.IO;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for <see cref="Xml"/> objects.
/// </summary>
public class XmlSerializer : EncodableDataTypeSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="XmlSerializer"/> class.
    /// </summary>
    public XmlSerializer()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XmlSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public XmlSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(Xml);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        return obj is not Xml photo ? null : Encode(photo, photo.Value);
    }

    /// <summary>
    /// Deserializes the specified value into a <see cref="Xml"/> object.
    /// </summary>
    /// <param name="value">The string value to deserialize.</param>
    /// <returns>The deserialized <see cref="Xml"/> object.</returns>
    public Xml Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not Xml xml)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(xml, value);

        if (value is null)
        {
            return null;
        }

        xml.Value = value;

        return xml;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}
