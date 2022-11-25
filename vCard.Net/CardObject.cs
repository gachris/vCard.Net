using System;
using System.Runtime.Serialization;
using vCard.Net.Collections;

namespace vCard.Net
{
    /// <summary>
    /// The base class for all vCard objects and components.
    /// </summary>
    public class CardObject : CardObjectBase, ICardObject
    {
        private ICardObjectList<ICardObject> _children;
        private ServiceProvider _serviceProvider;

        internal CardObject() => Initialize();

        public CardObject(string name) : this() => Name = name;

        public CardObject(int line, int col) : this()
        {
            Line = line;
            Column = col;
        }

        private void Initialize()
        {
            //ToDo: I'm fairly certain this is ONLY used for null checking. If so, maybe it can just be a bool? vCardObjectList is an empty object, and
            //ToDo: its constructor parameter is ignored
            _children = new CardObjectList(this);
            _serviceProvider = new ServiceProvider();

            _children.ItemAdded += Children_ItemAdded;
        }

        [OnDeserializing]
        internal void DeserializingInternal(StreamingContext context) => OnDeserializing(context);

        [OnDeserialized]
        internal void DeserializedInternal(StreamingContext context) => OnDeserialized(context);

        protected virtual void OnDeserializing(StreamingContext context) => Initialize();

        protected virtual void OnDeserialized(StreamingContext context) { }

        private void Children_ItemAdded(object sender, ObjectEventArgs<ICardObject, int> e) => e.First.Parent = this;

        protected bool Equals(CardObject other) => string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((CardObject)obj);
        }

        public override int GetHashCode() => Name?.GetHashCode() ?? 0;

        public override void CopyFrom(ICopyable c)
        {
            var obj = c as ICardObject;
            if (obj == null)
            {
                return;
            }

            // Copy the name and basic information
            Name = obj.Name;
            Parent = obj.Parent;
            Line = obj.Line;
            Column = obj.Column;

            // Add each child
            Children.Clear();
            foreach (var child in obj.Children)
            {
                this.AddChild(child);
            }
        }

        /// <summary>
        /// Returns the parent CardObject that owns this one.
        /// </summary>
        public virtual ICardObject Parent { get; set; }

        /// <summary>
        /// A collection of CardObject that are children of the current object.
        /// </summary>
        public virtual ICardObjectList<ICardObject> Children => _children;

        /// <summary>
        /// Gets or sets the name of the CardObject.
        /// </summary>        
        public virtual string Name { get; set; }

        public virtual int Line { get; set; }

        public virtual int Column { get; set; }

        public virtual object GetService(Type serviceType) => _serviceProvider.GetService(serviceType);

        public virtual object GetService(string name) => _serviceProvider.GetService(name);

        public virtual T GetService<T>() => _serviceProvider.GetService<T>();

        public virtual T GetService<T>(string name) => _serviceProvider.GetService<T>(name);

        public virtual void SetService(string name, object obj) => _serviceProvider.SetService(name, obj);

        public virtual void SetService(object obj) => _serviceProvider.SetService(obj);

        public virtual void RemoveService(Type type) => _serviceProvider.RemoveService(type);

        public virtual void RemoveService(string name) => _serviceProvider.RemoveService(name);

        public virtual string Group
        {
            get => Name;
            set => Name = value;
        }
    }
}