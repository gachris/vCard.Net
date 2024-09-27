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
public class vCardComponent : vCardObject, IvCardComponent
{
    /// <summary>
    /// Gets a list of properties that are associated with the vCard component.
    /// </summary>
    public virtual vCardPropertyList Properties { get; protected set; }

    /// <summary>
    /// Gets or sets the vCard version associated with this object.
    /// </summary>
    public virtual vCardVersion Version
    {
        get => Properties.Get<string>("VERSION").FromVersionString();
        set => Properties.Set("VERSION", value.ToVersionString());
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="vCardComponent"/> class.
    /// </summary>
    public vCardComponent() : base() => Initialize();

    /// <summary>
    /// Initializes a new instance of the <see cref="vCardComponent"/> class with the specified name.
    /// </summary>
    /// <param name="name">The name of the component.</param>
    public vCardComponent(string name) : base(name) => Initialize();

    /// <summary>
    /// Initializes the component by creating a new property list.
    /// </summary>
    private void Initialize() => Properties = new vCardPropertyList(this);

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

        if (obj is not IvCardComponent c)
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
        var p = new vCardProperty(name, value);
        AddProperty(p);
    }

    /// <summary>
    /// Adds a property to this component.
    /// </summary>
    /// <param name="p">The property to add.</param>
    public virtual void AddProperty(IvCardProperty p)
    {
        p.Parent = this;
        Properties.Set(p.Name, p.Value);
    }
}