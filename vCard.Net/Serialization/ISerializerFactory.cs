using System;

namespace vCard.Net.Serialization
{
    public interface ISerializerFactory
    {
        ISerializer Build(Type objectType, SerializationContext ctx);
    }
}