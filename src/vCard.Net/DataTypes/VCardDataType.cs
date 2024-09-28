using System.Runtime.Serialization;
using vCard.Net.Proxies;

namespace vCard.Net.DataTypes;

/// <summary>
/// An abstract class from which all vCard data types inherit.
/// </summary>
/// <remarks>
/// This abstract class serves as the foundation for all vCard data types. It provides functionality
/// for managing parameters, version compatibility, and associated vCard objects.
/// </remarks>
public abstract class VCardDataType : IVCardDataType
{
    private IParameterCollection _parameters;
    private ParameterCollectionProxy _proxy;
    private ServiceProvider _serviceProvider;
    private IVCardObject _associatedObject;

    /// <summary>
    /// Initializes a new instance of the <see cref="VCardDataType"/> class.
    /// </summary>
    protected VCardDataType()
    {
        Initialize();
    }

    private void Initialize()
    {
        _parameters = new ParameterList();
        _proxy = new ParameterCollectionProxy(_parameters);
        _serviceProvider = new ServiceProvider();
    }

    [OnDeserializing]
    internal void DeserializingInternal(StreamingContext context) => OnDeserializing(context);

    [OnDeserialized]
    internal void DeserializedInternal(StreamingContext context) => OnDeserialized(context);

    /// <summary>
    /// Method called during deserialization to initialize the object.
    /// </summary>
    protected virtual void OnDeserializing(StreamingContext context) => Initialize();

    /// <summary>
    /// Method called after deserialization is complete.
    /// </summary>
    protected virtual void OnDeserialized(StreamingContext context) { }

    /// <inheritdoc/>
    public virtual string ValueType
    {
        get => _proxy.Get("VALUE");
        set => SetValueType(value);
    }

    /// <inheritdoc/>
    public virtual Type GetValueType()
    {
        // See RFC 5545 Section 3.2.20.
        if (_proxy != null && _proxy.ContainsKey("VALUE"))
        {
            return _proxy.Get("VALUE") switch
            {
                "BINARY" => typeof(byte[]),
                "BOOLEAN" => typeof(bool),
                "CAL-ADDRESS" => typeof(Uri),
                "DATE" => typeof(IDateTime),
                "DATE-AND-OR-TIME" => typeof(IDateTime),
                "DATE-TIME" => typeof(IDateTime),
                "FLOAT" => typeof(double),
                "DURATION" => typeof(TimeSpan),
                "INTEGER" => typeof(int),
                "TEXT" => typeof(string),
                "TIME" => throw new NotImplementedException(),// FIXME: implement ISO.8601.2004
                "URI" => typeof(Uri),
                _ => null,
            };
        }
        return null;
    }

    /// <inheritdoc/>
    public virtual void SetValueType(string type) => _proxy?.Set("VALUE", type ?? type.ToUpper());

    /// <inheritdoc/>
    public virtual IVCardObject AssociatedObject
    {
        get => _associatedObject;
        set
        {
            if (Equals(_associatedObject, value))
            {
                return;
            }

            _associatedObject = value;

            if (_associatedObject != null)
            {
                _proxy.SetParent(_associatedObject);
                if (_associatedObject is IVCardParameterCollectionContainer container)
                {
                    _proxy.SetProxiedObject(container.Parameters);
                }
            }
            else
            {
                _proxy.SetParent(null);
                _proxy.SetProxiedObject(_parameters);
            }
        }
    }

    /// <inheritdoc/>
    public virtual string Language
    {
        get => Parameters.Get("LANGUAGE");
        set => Parameters.Set("LANGUAGE", value);
    }

    /// <inheritdoc/>
    public virtual void CopyFrom(ICopyable obj)
    {
        if (obj is not IVCardDataType)
        {
            return;
        }

        var dt = (IVCardDataType)obj;
        _associatedObject = dt.AssociatedObject;
        _proxy.SetParent(_associatedObject);
        _proxy.SetProxiedObject(dt.Parameters);
    }

    /// <inheritdoc/>
    public virtual T Copy<T>()
    {
        var type = GetType();
        var obj = Activator.CreateInstance(type) as ICopyable;

        // Duplicate our values
        if (obj is T)
        {
            obj.CopyFrom(this);
            return (T)obj;
        }
        return default;
    }

    /// <inheritdoc/>
    public virtual IParameterCollection Parameters => _proxy;

    /// <inheritdoc/>
    public virtual object GetService(Type serviceType) => _serviceProvider.GetService(serviceType);

    /// <inheritdoc/>
    public object GetService(string name) => _serviceProvider.GetService(name);

    /// <inheritdoc/>
    public T GetService<T>() => _serviceProvider.GetService<T>();

    /// <inheritdoc/>
    public T GetService<T>(string name) => _serviceProvider.GetService<T>(name);

    /// <inheritdoc/>
    public void SetService(string name, object obj) => _serviceProvider.SetService(name, obj);

    /// <inheritdoc/>
    public void SetService(object obj) => _serviceProvider.SetService(obj);

    /// <inheritdoc/>
    public void RemoveService(Type type) => _serviceProvider.RemoveService(type);

    /// <inheritdoc/>
    public void RemoveService(string name) => _serviceProvider.RemoveService(name);
}