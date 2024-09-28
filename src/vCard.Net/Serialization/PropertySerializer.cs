using System.Text;
using vCard.Net.CardComponents;
using vCard.Net.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.Serialization;

/// <summary>
/// Serializes vCard properties to a string format.
/// </summary>
public class PropertySerializer : SerializerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PropertySerializer"/> class.
    /// </summary>
    public PropertySerializer() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PropertySerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public PropertySerializer(SerializationContext ctx) : base(ctx) { }

    /// <inheritdoc/>
    public override Type TargetType => typeof(VCardProperty);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        var prop = obj as IVCardProperty;
        if (prop?.Values == null || !prop.Values.Any())
        {
            return null;
        }

        // Push this object on the serialization context.
        SerializationContext.Push(prop);

        // Get a serializer factory that we can use to serialize
        // the property and parameter values
        var sf = GetService<ISerializerFactory>();

        var result = new StringBuilder();
        foreach (var v in prop.Values.Where(value => value != null))
        {
            // Get a serializer to serialize the property's value.
            // If we can't serialize the property's value, the next step is worthless anyway.
            var valueSerializer = sf.Build(v.GetType(), SerializationContext) as IStringSerializer;

            // Iterate through each value to be serialized,
            // and give it a property (with parameters).
            // FIXME: this isn't always the way this is accomplished.
            // Multiple values can often be serialized within the
            // same property.  How should we fix this?

            // NOTE:
            // We Serialize the property's value first, as during 
            // serialization it may modify our parameters.
            // FIXME: the "parameter modification" operation should
            // be separated from serialization. Perhaps something
            // like PreSerialize(), etc.
            var value = valueSerializer.SerializeToString(v);

            // Get the list of parameters we'll be serializing
            var parameterList = prop.Parameters;
            if (v is IVCardDataType)
            {
                parameterList = (v as IVCardDataType).Parameters;
            }

            var sb = new StringBuilder();
            sb.Append(prop.Name);
            if (parameterList.Any())
            {
                // Get a serializer for parameters
                if (sf.Build(typeof(VCardParameter), SerializationContext) is IStringSerializer parameterSerializer)
                {
                    // Serialize each parameter
                    // Separate parameters with semicolons
                    sb.Append(";");
                    sb.Append(string.Join(";", parameterList.Select(parameterSerializer.SerializeToString)));
                }
            }
            sb.Append(":");
            sb.Append(value);

            result.Append(TextUtil.FoldLines(sb.ToString()));

            var property = SerializationContext.Peek() as IVCardProperty;
            var vCardVersion = property.Parent is IVCardComponent component ? component.Version : VCardVersion.vCard2_1;

            if (v is Address address && vCardVersion is not VCardVersion.vCard4_0)
            {
                var labelSerializer = sf.Build(typeof(Label), SerializationContext) as IStringSerializer;
              
                result.Append("LABEL");
                
                if (sf.Build(typeof(VCardParameter), SerializationContext) is IStringSerializer parameterSerializer)
                {
                    result.Append(";");
                    result.Append(string.Join(";", parameterList.Select(parameterSerializer.SerializeToString)));
                }

                result.Append(":");

                result.AppendLine(labelSerializer.SerializeToString(address.Label));
            }
        }

        // Pop the object off the serialization context.
        SerializationContext.Pop();
        return result.ToString();
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => null;
}