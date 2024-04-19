using System;
using System.Runtime.Serialization;
using vCard.Net.Collections;
using vCard.Net.DataTypes;

namespace vCard.Net;

/// <summary>
/// The base class for all vCard objects and components.
/// </summary>
public class vCardObject : vCardObjectBase, IvCardObject
{
    private IvCardObjectList<IvCardObject> _children;
    private ServiceProvider _serviceProvider;

    /// <summary>
    /// Gets or sets the vCard version associated with this object.
    /// </summary>
    public vCardVersion Version { get; set; }
   
    /// <summary>
    /// Initializes a new instance of the <see cref="vCardObject"/> class.
    /// </summary>
    internal vCardObject() => Initialize();

    /// <summary>
    /// Initializes a new instance of the <see cref="vCardObject"/> class with the specified name.
    /// </summary>
    public vCardObject(string name) : this() => Name = name;

    /// <summary>
    /// Initializes a new instance of the <see cref="vCardObject"/> class with the specified line and column numbers.
    /// </summary>
    public vCardObject(int line, int col) : this()
    {
        Line = line;
        Column = col;
    }

    /// <summary>
    /// Initializes the vCard object with necessary components.
    /// </summary>
    private void Initialize()
    {
        _children = new vCardObjectList(this);
        _serviceProvider = new ServiceProvider();

        _children.ItemAdded += Children_ItemAdded;
    }

    /// <summary>
    /// Method invoked when the object is being deserialized.
    /// </summary>
    [OnDeserializing]
    internal void DeserializingInternal(StreamingContext context) => OnDeserializing(context);

    /// <summary>
    /// Method invoked after the object has been deserialized.
    /// </summary>
    [OnDeserialized]
    internal void DeserializedInternal(StreamingContext context) => OnDeserialized(context);
    
    /// <summary>
    /// Initializes necessary components during deserialization.
    /// </summary>
    protected virtual void OnDeserializing(StreamingContext context) => Initialize();

    /// <summary>
    /// Placeholder for post-deserialization initialization logic.
    /// </summary>
    protected virtual void OnDeserialized(StreamingContext context) { }

    /// <summary>
    /// Event handler for when an item is added to the children collection.
    /// </summary>
    private void Children_ItemAdded(object sender, ObjectEventArgs<IvCardObject, int> e) => e.First.Parent = this;

    /// <summary>
    /// Determines whether the current vCard object is equal to another vCard object.
    /// </summary>
    protected bool Equals(vCardObject other) => string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);
   
    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return ReferenceEquals(null, obj) ? false : ReferenceEquals(this, obj) ? true : obj.GetType() == GetType() && Equals((vCardObject)obj);
    }

    /// <inheritdoc/>
    public override int GetHashCode() => Name?.GetHashCode() ?? 0;

    /// <inheritdoc/>
    public override void CopyFrom(ICopyable c)
    {
        if (c is not IvCardObject obj)
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
    /// Gets or sets the parent vCard object that owns this one.
    /// </summary>
    public virtual IvCardObject Parent { get; set; }

    /// <summary>
    /// Gets a collection of vCard objects that are children of the current object.
    /// </summary>
    public virtual IvCardObjectList<IvCardObject> Children => _children;

    /// <summary>
    /// Gets or sets the name of the vCard object.
    /// </summary>        
    public virtual string Name { get; set; }

    /// <summary>
    /// Gets or sets the line number where this vCard object was found during parsing.
    /// </summary>
    public virtual int Line { get; set; }

    /// <summary>
    /// Gets or sets the column number where this vCard object was found during parsing.
    /// </summary>
    public virtual int Column { get; set; }

    /// <inheritdoc/>
    public virtual object GetService(Type serviceType) => _serviceProvider.GetService(serviceType);

    /// <inheritdoc/>
    public virtual object GetService(string name) => _serviceProvider.GetService(name);

    /// <inheritdoc/>
    public virtual T GetService<T>() => _serviceProvider.GetService<T>();

    /// <inheritdoc/>
    public virtual T GetService<T>(string name) => _serviceProvider.GetService<T>(name);

    /// <inheritdoc/>
    public virtual void SetService(string name, object obj) => _serviceProvider.SetService(name, obj);

    /// <inheritdoc/>
    public virtual void SetService(object obj) => _serviceProvider.SetService(obj);

    /// <inheritdoc/>
    public virtual void RemoveService(Type type) => _serviceProvider.RemoveService(type);

    /// <inheritdoc/>
    public virtual void RemoveService(string name) => _serviceProvider.RemoveService(name);

    /// <summary>
    /// Gets or sets the group name of the vCard object.
    /// </summary>
    public virtual string Group
    {
        get => Name;
        set => Name = value;
    }
}