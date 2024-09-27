using System;
using System.IO;
using System.Text;

namespace vCard.Net.Serialization;

/// <summary>
/// Base class for serializers that handle serialization and deserialization of objects.
/// </summary>
public abstract class SerializerBase : IStringSerializer
{
    private SerializationContext _mSerializationContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="SerializerBase"/> class with the default serialization context.
    /// </summary>
    protected SerializerBase()
    {
        _mSerializationContext = SerializationContext.Default;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SerializerBase"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context to use.</param>
    protected SerializerBase(SerializationContext ctx)
    {
        _mSerializationContext = ctx;
    }

    /// <inheritdoc/>
    public virtual SerializationContext SerializationContext
    {
        get => _mSerializationContext;
        set => _mSerializationContext = value;
    }

    /// <inheritdoc/>
    public abstract Type TargetType { get; }

    /// <summary>
    /// Serializes the specified object to a string representation.
    /// </summary>
    /// <param name="obj">The object to serialize.</param>
    /// <returns>The string representation of the serialized object.</returns>
    public abstract string SerializeToString(object obj);

    /// <summary>
    /// Deserializes an object from the specified TextReader.
    /// </summary>
    /// <param name="tr">The TextReader from which to deserialize the object.</param>
    /// <returns>The deserialized object.</returns>
    public abstract object Deserialize(TextReader tr);

    /// <inheritdoc/>
    public object Deserialize(Stream stream, Encoding encoding)
    {
        object obj;
        using (var sr = new StreamReader(stream, encoding))
        {
            var encodingStack = GetService<EncodingStack>();
            encodingStack.Push(encoding);
            obj = Deserialize(sr);
            encodingStack.Pop();
        }
        return obj;
    }

    /// <inheritdoc/>
    public void Serialize(object obj, Stream stream, Encoding encoding)
    {
        // NOTE: we don't use a 'using' statement here because
        // we don't want the stream to be closed by this serialization.
        // Fixes bug #3177278 - Serialize closes stream

        const int defaultBuffer = 1024;     //This is StreamWriter's built-in default buffer size
        using var sw = new StreamWriter(stream, encoding, defaultBuffer, leaveOpen: true);
        // Push the current object onto the serialization stack
        SerializationContext.Push(obj);

        // Push the current encoding on the stack
        var encodingStack = GetService<EncodingStack>();
        encodingStack.Push(encoding);

        sw.Write(SerializeToString(obj));

        // Pop the current encoding off the serialization stack
        encodingStack.Pop();

        // Pop the current object off the serialization stack
        SerializationContext.Pop();
    }

    /// <inheritdoc/>
    public virtual object GetService(Type serviceType) => SerializationContext?.GetService(serviceType);

    /// <inheritdoc/>
    public virtual object GetService(string name) => SerializationContext?.GetService(name);

    /// <inheritdoc/>
    public virtual T GetService<T>()
    {
        return SerializationContext != null ? SerializationContext.GetService<T>() : default;
    }

    /// <inheritdoc/>
    public virtual T GetService<T>(string name)
    {
        return SerializationContext != null ? SerializationContext.GetService<T>(name) : default;
    }

    /// <inheritdoc/>
    public void SetService(string name, object obj) => SerializationContext?.SetService(name, obj);

    /// <inheritdoc/>
    public void SetService(object obj) => SerializationContext?.SetService(obj);

    /// <inheritdoc/>
    public void RemoveService(Type type) => SerializationContext?.RemoveService(type);

    /// <inheritdoc/>
    public void RemoveService(string name) => SerializationContext?.RemoveService(name);
}