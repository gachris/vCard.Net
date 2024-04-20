using System.Linq;
using vCard.Net.Collections;

namespace vCard.Net;

/// <summary>
/// Represents a collection of vCard properties grouped by their names.
/// </summary>
public class vCardPropertyList : GroupedValueList<string, IvCardProperty, vCardProperty, object>
{
    private readonly IvCardObject _mParent;

    /// <summary>
    /// Initializes a new instance of the <see cref="vCardPropertyList"/> class.
    /// </summary>
    public vCardPropertyList() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="vCardPropertyList"/> class with the specified parent object.
    /// </summary>
    /// <param name="parent">The parent object to which this property list belongs.</param>
    public vCardPropertyList(IvCardObject parent)
    {
        _mParent = parent;
        ItemAdded += vCardPropertyList_ItemAdded;
    }

    private void vCardPropertyList_ItemAdded(object sender, ObjectEventArgs<IvCardProperty, int> e)
    {
        e.First.Parent = _mParent;
    }

    /// <summary>
    /// Gets the first vCard property with the specified name.
    /// </summary>
    /// <param name="name">The name of the property to retrieve.</param>
    /// <returns>The first vCard property with the specified name, or <c>null</c> if no such property exists.</returns>
    public IvCardProperty this[string name] => ContainsKey(name) ? AllOf(name).FirstOrDefault() : null;
}