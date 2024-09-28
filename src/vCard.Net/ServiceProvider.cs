namespace vCard.Net;

/// <summary>
/// Provides a simple implementation of a service provider that allows registration and retrieval of services by type or name.
/// </summary>
public class ServiceProvider
{
    private readonly IDictionary<Type, object> _mTypedServices = new Dictionary<Type, object>();
    private readonly IDictionary<string, object> _mNamedServices = new Dictionary<string, object>();

    /// <summary>
    /// Retrieves a service of the specified type.
    /// </summary>
    public virtual object GetService(Type serviceType)
    {
        _mTypedServices.TryGetValue(serviceType, out object service);
        return service;
    }

    /// <summary>
    /// Retrieves a service with the specified name.
    /// </summary>
    public virtual object GetService(string name)
    {
        _mNamedServices.TryGetValue(name, out object service);
        return service;
    }

    /// <summary>
    /// Retrieves a service of the specified type.
    /// </summary>
    public virtual T GetService<T>()
    {
        var service = GetService(typeof(T));
        return service is T ? (T)service : default;
    }

    /// <summary>
    /// Retrieves a service of the specified type with the specified name.
    /// </summary>
    public virtual T GetService<T>(string name)
    {
        var service = GetService(name);
        return service is T ? (T)service : default;
    }

    /// <summary>
    /// Registers a service with the specified name.
    /// </summary>
    public virtual void SetService(string name, object obj)
    {
        if (!string.IsNullOrEmpty(name) && obj != null)
        {
            _mNamedServices[name] = obj;
        }
    }

    /// <summary>
    /// Registers a service.
    /// </summary>
    public virtual void SetService(object obj)
    {
        if (obj != null)
        {
            var type = obj.GetType();
            _mTypedServices[type] = obj;

            // Get interfaces for the given type
            foreach (var iface in type.GetInterfaces())
            {
                _mTypedServices[iface] = obj;
            }
        }
    }

    /// <summary>
    /// Removes the service associated with the specified type.
    /// </summary>
    public virtual void RemoveService(Type type)
    {
        if (type != null)
        {
            if (_mTypedServices.ContainsKey(type))
            {
                _mTypedServices.Remove(type);
            }

            // Get interfaces for the given type
            foreach (var iface in type.GetInterfaces().Where(iface => _mTypedServices.ContainsKey(iface)))
            {
                _mTypedServices.Remove(iface);
            }
        }
    }

    /// <summary>
    /// Removes the service associated with the specified name.
    /// </summary>
    public virtual void RemoveService(string name)
    {
        if (_mNamedServices.ContainsKey(name))
        {
            _mNamedServices.Remove(name);
        }
    }
}