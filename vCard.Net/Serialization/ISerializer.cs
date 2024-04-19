using System;
using System.IO;
using System.Text;

namespace vCard.Net.Serialization;

/// <summary>
/// Interface for serializers used to serialize and deserialize objects.
/// </summary>
public interface ISerializer : IServiceProvider
{
    /// <summary>
    /// Gets or sets the serialization context associated with the serializer.
    /// </summary>
    SerializationContext SerializationContext { get; set; }

    /// <summary>
    /// Gets the type of objects that the serializer can handle.
    /// </summary>
    Type TargetType { get; }

    /// <summary>
    /// Serializes the specified object to the specified stream using the specified encoding.
    /// </summary>
    /// <param name="obj">The object to serialize.</param>
    /// <param name="stream">The stream to which the object will be serialized.</param>
    /// <param name="encoding">The encoding to use for serialization.</param>
    void Serialize(object obj, Stream stream, Encoding encoding);

    /// <summary>
    /// Deserializes an object from the specified stream using the specified encoding.
    /// </summary>
    /// <param name="stream">The stream from which to deserialize the object.</param>
    /// <param name="encoding">The encoding used for deserialization.</param>
    /// <returns>The deserialized object.</returns>
    object Deserialize(Stream stream, Encoding encoding);
}