using System;
using System.IO;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.Serialization;

/// <summary>
/// Serializer for mapping and serializing data objects based on their data types.
/// </summary>
public class DataMapSerializer : SerializerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataMapSerializer"/> class.
    /// </summary>
    public DataMapSerializer() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataMapSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public DataMapSerializer(SerializationContext ctx) : base(ctx) { }

    /// <summary>
    /// Gets the mapped serializer based on the data type of the object being serialized.
    /// </summary>
    /// <returns>The mapped serializer.</returns>
    protected IStringSerializer GetMappedSerializer()
    {
        var sf = GetService<ISerializerFactory>();
        var mapper = GetService<DataTypeMapper>();
        if (sf == null || mapper == null)
        {
            return null;
        }

        var obj = SerializationContext.Peek();

        // Get the data type for this object
        var type = mapper.GetPropertyMapping(obj);

        return type == null
            ? new StringSerializer(SerializationContext)
            : sf.Build(type, SerializationContext) as IStringSerializer;
    }

    /// <inheritdoc/>
    public override Type TargetType
    {
        get
        {
            ISerializer serializer = GetMappedSerializer();
            return serializer?.TargetType;
        }
    }

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        var serializer = GetMappedSerializer();
        return serializer?.SerializeToString(obj);
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr)
    {
        var serializer = GetMappedSerializer();
        if (serializer == null)
        {
            return null;
        }

        var value = tr.ReadToEnd();
        var returnValue = serializer.Deserialize(new StringReader(value));

        // Default to returning the string representation of the value
        // if the value wasn't formatted correctly.
        // FIXME: should this be a try/catch?  Should serializers be throwing
        // an InvalidFormatException?  This may have some performance issues
        // as try/catch is much slower than other means.
        return returnValue ?? value;
    }
}