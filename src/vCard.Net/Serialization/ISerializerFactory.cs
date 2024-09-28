namespace vCard.Net.Serialization;

/// <summary>
/// Interface for serializer factories.
/// </summary>
public interface ISerializerFactory
{
    /// <summary>
    /// Builds a serializer for the specified object type and serialization context.
    /// </summary>
    /// <param name="objectType">The type of object to be serialized.</param>
    /// <param name="ctx">The serialization context.</param>
    /// <returns>The serializer instance.</returns>
    ISerializer Build(Type objectType, SerializationContext ctx);
}