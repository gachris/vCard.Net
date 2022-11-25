using System;
using System.Runtime.Serialization;
using vCard.Net.Proxies;

namespace vCard.Net.DataTypes
{
    /// <summary>
    /// An abstract class from which all vCard data types inherit.
    /// </summary>
    public abstract class CardDataType : ICardDataType
    {
        private SpecificationVersions _version;
        private IParameterCollection _parameters;
        private ParameterCollectionProxy _proxy;
        private ServiceProvider _serviceProvider;

        protected ICardObject _AssociatedObject;

        /// <summary>
        /// All objects derived from this class must implement this to indicate the specification versions with which they can be used.
        /// </summary>
        public abstract SpecificationVersions VersionsSupported { get; }

        /// <summary>
        /// This is used to establish the specification version to which the PDI object will
        /// conform when converted to its string form.
        /// </summary>
        /// <value>
        /// Some PDI objects are handled differently under the different versions of the
        /// specifications. This property allows them to react accordingly. If the object
        /// is owned by another object, the owner should set the version as needed before
        /// doing anything that is version dependent such as converting it to a string. This
        /// should also be overridden where necessary to propagate the version to all owned
        /// objects. Derived classes with only one version or with version-dependent behavior
        /// should always set the default version when constructed.
        /// </value>
        /// <exception cref="ArgumentException">
        /// This exception is thrown if an attempt is made to set the version to None, a
        /// combination of version values, or if the specified version is not supported by
        /// the object.
        /// </exception>
        public virtual SpecificationVersions Version
        {
            get => _version;
            set
            {
                switch (value)
                {
                    case SpecificationVersions.None:
                        throw new ArgumentException("ExPDIOVersionSetToNone");
                    default:
                        throw new ArgumentException("ExPDIOVersionCombo");
                    case SpecificationVersions.vCard21:
                    case SpecificationVersions.vCard30:
                    case SpecificationVersions.vCard40:
                        if ((VersionsSupported & value) == 0)
                        {
                            throw new ArgumentException("ExPDIOVersionNotSupported");
                        }

                        _version = value;
                        break;
                }
            }
        }

        protected CardDataType() => Initialize();

        private void Initialize()
        {
            _version = SpecificationVersions.None;
            _parameters = new ParameterList();
            _proxy = new ParameterCollectionProxy(_parameters);
            _serviceProvider = new ServiceProvider();
        }

        [OnDeserializing]
        internal void DeserializingInternal(StreamingContext context) => OnDeserializing(context);

        [OnDeserialized]
        internal void DeserializedInternal(StreamingContext context) => OnDeserialized(context);

        protected virtual void OnDeserializing(StreamingContext context) => Initialize();

        protected virtual void OnDeserialized(StreamingContext context) { }

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

        public virtual void SetValueType(string type) => _proxy?.Set("VALUE", type ?? type.ToUpper());

        public virtual ICardObject AssociatedObject
        {
            get => _AssociatedObject;
            set
            {
                if (Equals(_AssociatedObject, value))
                {
                    return;
                }

                _AssociatedObject = value;
                if (_AssociatedObject != null)
                {
                    _proxy.SetParent(_AssociatedObject);
                    if (_AssociatedObject is ICardParameterCollectionContainer)
                    {
                        _proxy.SetProxiedObject(((ICardParameterCollectionContainer)_AssociatedObject).Parameters);
                    }
                }
                else
                {
                    _proxy.SetParent(null);
                    _proxy.SetProxiedObject(_parameters);
                }
            }
        }

        public virtual string Language
        {
            get => Parameters.Get("LANGUAGE");
            set => Parameters.Set("LANGUAGE", value);
        }

        /// <summary>
        /// Copies values from the target object to the
        /// current object.
        /// </summary>
        public virtual void CopyFrom(ICopyable obj)
        {
            if (!(obj is ICardDataType))
            {
                return;
            }

            var dt = (ICardDataType)obj;
            _AssociatedObject = dt.AssociatedObject;
            _proxy.SetParent(_AssociatedObject);
            _proxy.SetProxiedObject(dt.Parameters);
        }

        /// <summary>
        /// Creates a copy of the object.
        /// </summary>
        /// <returns>The copy of the object.</returns>
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

        public virtual IParameterCollection Parameters => _proxy;

        public virtual object GetService(Type serviceType) => _serviceProvider.GetService(serviceType);

        public object GetService(string name) => _serviceProvider.GetService(name);

        public T GetService<T>() => _serviceProvider.GetService<T>();

        public T GetService<T>(string name) => _serviceProvider.GetService<T>(name);

        public void SetService(string name, object obj) => _serviceProvider.SetService(name, obj);

        public void SetService(object obj) => _serviceProvider.SetService(obj);

        public void RemoveService(Type type) => _serviceProvider.RemoveService(type);

        public void RemoveService(string name) => _serviceProvider.RemoveService(name);
    }
}