using System.Diagnostics;
using System.Runtime.Serialization;
using vCard.Net.DataTypes;
using vCard.Net.Serialization;

namespace vCard.Net.CardComponents;

/// <summary>
/// Represents a component within a vCard object, used by the parsing framework for vCard components.
/// Generally, you should not need to use this class directly.
/// </summary>
[DebuggerDisplay("Component: {Name}")]
public class VCardComponent : VCardObject, IVCardComponent
{
    /// <summary>
    /// Gets a list of properties that are associated with the vCard component.
    /// </summary>
    public virtual VCardPropertyList Properties { get; protected set; }

    /// <summary>
    /// Gets or sets the vCard version associated with this object.
    /// </summary>
    public virtual VCardVersion Version
    {
        get => Properties.Get<string>("VERSION").FromVersionString();
        set => Properties.Set("VERSION", value.ToVersionString());
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="VCardComponent"/> class.
    /// </summary>
    public VCardComponent() : base() => Initialize();

    /// <summary>
    /// Initializes a new instance of the <see cref="VCardComponent"/> class with the specified name.
    /// </summary>
    /// <param name="name">The name of the component.</param>
    public VCardComponent(string name) : base(name) => Initialize();

    /// <summary>
    /// Initializes the component by creating a new property list.
    /// </summary>
    private void Initialize() => Properties = new VCardPropertyList(this);

    /// <inheritdoc/>
    protected override void OnDeserializing(StreamingContext context)
    {
        base.OnDeserializing(context);

        Initialize();
    }

    /// <inheritdoc/>
    public override void CopyFrom(ICopyable obj)
    {
        base.CopyFrom(obj);

        if (obj is not IVCardComponent c)
        {
            return;
        }

        Properties.Clear();
        foreach (var p in c.Properties)
        {
            Properties.Add(p);
        }
    }

    /// <summary>
    /// Adds a property to this component with the specified name and value.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="value">The value of the property.</param>
    public virtual void AddProperty(string name, string value)
    {
        var p = new VCardProperty(name, value);
        AddProperty(p);
    }

    /// <summary>
    /// Adds a property to this component.
    /// </summary>
    /// <param name="p">The property to add.</param>
    public virtual void AddProperty(IVCardProperty p)
    {
        p.Parent = this;
        Properties.Set(p.Name, p.Value);
    }
}