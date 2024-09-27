namespace vCard.Net.Serialization;

/// <summary>
/// Provides a context for serialization operations, managing services and object references.
/// </summary>
public class SerializationContext
{
    private static SerializationContext _default;

    /// <summary>
    /// Gets the Singleton instance of the SerializationContext class.
    /// </summary>
    public static SerializationContext Default
    {
        get
        {
            if (_default == null)
            {
                _default = new SerializationContext();
            }

            // Create a new serialization context that doesn't contain any objects
            // (and is non-static).  That way, if any objects get pushed onto
            // the serialization stack when the Default serialization context is used,
            // and something goes wrong and the objects don't get popped off the stack,
            // we don't need to worry (as much) about a memory leak, because the
            // objects weren't pushed onto a stack referenced by a static variable.
            var ctx = new SerializationContext
            {
                _mServiceProvider = _default._mServiceProvider
            };
            return ctx;
        }
    }

    private readonly Stack<WeakReference> _mStack = new Stack<WeakReference>();
    private ServiceProvider _mServiceProvider = new ServiceProvider();

    /// <summary>
    /// Initializes a new instance of the SerializationContext class with default services.
    /// </summary>
    public SerializationContext()
    {
        // Add some services by default
        SetService(new SerializerFactory());
        SetService(new vCardComponentFactory());
        SetService(new DataTypeMapper());
        SetService(new EncodingStack());
        SetService(new EncodingProvider(this));
    }

    /// <summary>
    /// Pushes an object onto the serialization stack.
    /// </summary>
    /// <param name="item">The object to push onto the stack.</param>
    public virtual void Push(object item)
    {
        if (item != null)
        {
            _mStack.Push(new WeakReference(item));
        }
    }

    /// <summary>
    /// Pops an object from the serialization stack.
    /// </summary>
    /// <returns>The object popped from the stack.</returns>
    public virtual object Pop()
    {
        if (_mStack.Count > 0)
        {
            var r = _mStack.Pop();
            if (r.IsAlive)
            {
                return r.Target;
            }
        }
        return null;
    }

    /// <summary>
    /// Peeks at the top object on the serialization stack without removing it.
    /// </summary>
    /// <returns>The object at the top of the stack.</returns>
    public virtual object Peek()
    {
        if (_mStack.Count > 0)
        {
            var r = _mStack.Peek();
            if (r.IsAlive)
            {
                return r.Target;
            }
        }
        return null;
    }

    /// <summary>
    /// Gets a service of the specified type from the service provider.
    /// </summary>
    /// <param name="serviceType">The type of service to retrieve.</param>
    /// <returns>The requested service object.</returns>
    public virtual object GetService(Type serviceType) => _mServiceProvider.GetService(serviceType);

    /// <summary>
    /// Gets a service with the specified name from the service provider.
    /// </summary>
    /// <param name="name">The name of the service to retrieve.</param>
    /// <returns>The requested service object.</returns>
    public virtual object GetService(string name) => _mServiceProvider.GetService(name);

    /// <summary>
    /// Gets a service of the specified type from the service provider.
    /// </summary>
    /// <typeparam name="T">The type of service to retrieve.</typeparam>
    /// <returns>The requested service object.</returns>
    public virtual T GetService<T>() => _mServiceProvider.GetService<T>();

    /// <summary>
    /// Gets a service with the specified name from the service provider.
    /// </summary>
    /// <typeparam name="T">The type of service to retrieve.</typeparam>
    /// <param name="name">The name of the service to retrieve.</param>
    /// <returns>The requested service object.</returns>
    public virtual T GetService<T>(string name) => _mServiceProvider.GetService<T>(name);

    /// <summary>
    /// Sets a service with the specified name in the service provider.
    /// </summary>
    /// <param name="name">The name of the service to set.</param>
    /// <param name="obj">The service object to set.</param>
    public virtual void SetService(string name, object obj) => _mServiceProvider.SetService(name, obj);

    /// <summary>
    /// Sets a service in the service provider.
    /// </summary>
    /// <param name="obj">The service object to set.</param>
    public virtual void SetService(object obj) => _mServiceProvider.SetService(obj);

    /// <summary>
    /// Removes a service of the specified type from the service provider.
    /// </summary>
    /// <param name="type">The type of service to remove.</param>
    public virtual void RemoveService(Type type) => _mServiceProvider.RemoveService(type);

    /// <summary>
    /// Removes a service with the specified name from the service provider.
    /// </summary>
    /// <param name="name">The name of the service to remove.</param>
    public virtual void RemoveService(string name) => _mServiceProvider.RemoveService(name);
}