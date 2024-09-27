using System;
using System.Reflection;
using vCard.Net.CardComponents;
using vCard.Net.DataTypes;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.Serialization;

/// <summary>
/// Factory class for creating serializers based on object types.
/// </summary>
public class SerializerFactory : ISerializerFactory
{
    private readonly ISerializerFactory _mDataTypeSerializerFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="SerializerFactory"/> class.
    /// </summary>
    public SerializerFactory() => _mDataTypeSerializerFactory = new DataTypeSerializerFactory();

    /// <summary>
    /// Returns a serializer that can be used to serialize an object of the specified type.
    /// </summary>
    /// <param name="objectType">The type of object to be serialized.</param>
    /// <param name="ctx">The serialization context.</param>
    /// <returns>An instance of <see cref="ISerializer"/>.</returns>
    public virtual ISerializer Build(Type objectType, SerializationContext ctx)
    {
        if (objectType == null)
        {
            return null;
        }
        ISerializer s;

        if (typeof(IvCardComponent).IsAssignableFrom(objectType))
        {
            s = new ComponentSerializer(ctx);
        }
        else if (typeof(IvCardProperty).IsAssignableFrom(objectType))
        {
            s = new PropertySerializer(ctx);
        }
        else if (typeof(vCardParameter).IsAssignableFrom(objectType))
        {
            s = new ParameterSerializer(ctx);
        }
        else if (typeof(string).IsAssignableFrom(objectType))
        {
            s = new StringSerializer(ctx);
        }
        else if (objectType.GetTypeInfo().IsEnum)
        {
            s = new EnumSerializer(objectType, ctx);
        }
        else if (typeof(TimeSpan).IsAssignableFrom(objectType))
        {
            s = new TimeSpanSerializer(ctx);
        }
        else if (typeof(int).IsAssignableFrom(objectType))
        {
            s = new IntegerSerializer(ctx);
        }
        else if (typeof(Uri).IsAssignableFrom(objectType))
        {
            s = new UriSerializer(ctx);
        }
        else if (typeof(IvCardDataType).IsAssignableFrom(objectType))
        {
            s = _mDataTypeSerializerFactory.Build(objectType, ctx);
        }
        // Default to a string serializer, which simply calls
        // ToString() on the value to serialize it.
        else
        {
            s = new StringSerializer(ctx);
        }

        return s;
    }
}