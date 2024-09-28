namespace vCard.Net.Serialization;

/// <summary>
/// Interface for serializers that work with string representations.
/// </summary>
public interface IStringSerializer : ISerializer
{
    /// <summary>
    /// Serializes the specified object to a string representation.
    /// </summary>
    /// <param name="obj">The object to serialize.</param>
    /// <returns>The string representation of the serialized object.</returns>
    string SerializeToString(object obj);

    /// <summary>
    /// Deserializes an object from the provided TextReader.
    /// </summary>
    /// <param name="tr">The TextReader from which to deserialize the object.</param>
    /// <returns>The deserialized object.</returns>
    object Deserialize(TextReader tr);
}