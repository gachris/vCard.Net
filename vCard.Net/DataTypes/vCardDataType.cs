using System;
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
public abstract class vCardDataType : IvCardDataType
{
    private IParameterCollection _parameters;
    private ParameterCollectionProxy _proxy;
    private ServiceProvider _serviceProvider;
    private IvCardObject _associatedObject;

    /// <summary>
    /// All objects derived from this class must implement this to indicate the specification versions with which they can be used.
    /// </summary>
    public abstract SpecificationVersions VersionsSupported { get; }
    
    /// <summary>
    /// Gets the vCard version associated with the object.
    /// </summary>
    /// <remarks>
    /// If no associated object is set, the default version is vCard 2.1.
    /// </remarks>
    public virtual vCardVersion Version => AssociatedObject?.Version ?? vCardVersion.vCard21;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="vCardDataType"/> class.
    /// </summary>
    protected vCardDataType()
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
    public virtual Type GetValueType()
    {
        // See RFC 5545 Section 3.2.20.
        if (_proxy != null && _proxy.ContainsKey("VALUE"))
        {
            switch (_proxy.Get("VALUE"))
            {
                case "BINARY":
                    return typeof(byte[]);
                case "BOOLEAN":
                    return typeof(bool);
                case "CAL-ADDRESS":
                    return typeof(Uri);
                case "DATE":
                    return typeof(IDateTime);
                case "DATE-AND-OR-TIME":
                    return typeof(IDateTime);
                case "DATE-TIME":
                    return typeof(IDateTime);
                case "FLOAT":
                    return typeof(double);
                case "DURATION":
                    return typeof(TimeSpan);
                case "INTEGER":
                    return typeof(int);
                case "TEXT":
                    return typeof(string);
                case "TIME":
                    throw new NotImplementedException();// FIXME: implement ISO.8601.2004
                case "URI":
                    return typeof(Uri);
                default:
                    return null;
            }
        }
        return null;
    }

    /// <inheritdoc/>
    public virtual void SetValueType(string type) => _proxy?.Set("VALUE", type ?? type.ToUpper());

    /// <inheritdoc/>
    public virtual IvCardObject AssociatedObject
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
                if (_associatedObject is IvCardParameterCollectionContainer)
                {
                    _proxy.SetProxiedObject(((IvCardParameterCollectionContainer)_associatedObject).Parameters);
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
        if (!(obj is IvCardDataType))
        {
            return;
        }

        var dt = (IvCardDataType)obj;
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