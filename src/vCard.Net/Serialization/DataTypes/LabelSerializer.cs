using System;
using System.IO;
using System.Net;
using vCard.Net.CardComponents;
using vCard.Net.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for <see cref="Label"/> objects.
/// </summary>
public class LabelSerializer : EncodableDataTypeSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LabelSerializer"/> class.
    /// </summary>
    public LabelSerializer()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LabelSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public LabelSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(Label);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        if (obj is not Label label)
        {
            return null;
        }

        var version = vCardVersion.vCard2_1;
        if (SerializationContext.Peek() is IvCardProperty property && property.Parent is IvCardComponent component)
        {
            version = component.Version;
        }

        var value = version == vCardVersion.vCard2_1 ? label.Value.RestrictedEscape() : label.Value.Escape();

        return Encode(label, value);
    }

    /// <summary>
    /// Deserializes the specified value into a <see cref="Label"/> object.
    /// </summary>
    /// <param name="value">The string value to deserialize.</param>
    /// <returns>The deserialized <see cref="Label"/> object.</returns>
    public Label Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not Label label)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(label, value);

        if (value is null)
        {
            return null;
        }

        label.Value = value.Unescape();

        return label;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}