using vCard.Net.Collections;

namespace vCard.Net;

/// <summary>
/// Represents a collection of vCard properties grouped by their names.
/// </summary>
public class VCardPropertyList : GroupedValueList<string, IVCardProperty, VCardProperty, object>
{
    private readonly IVCardObject _mParent;

    /// <summary>
    /// Initializes a new instance of the <see cref="VCardPropertyList"/> class.
    /// </summary>
    public VCardPropertyList() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="VCardPropertyList"/> class with the specified parent object.
    /// </summary>
    /// <param name="parent">The parent object to which this property list belongs.</param>
    public VCardPropertyList(IVCardObject parent)
    {
        _mParent = parent;
        ItemAdded += VCardPropertyList_ItemAdded;
    }

    private void VCardPropertyList_ItemAdded(object sender, ObjectEventArgs<IVCardProperty, int> e)
    {
        e.First.Parent = _mParent;
    }

    /// <summary>
    /// Gets the first vCard property with the specified name.
    /// </summary>
    /// <param name="name">The name of the property to retrieve.</param>
    /// <returns>The first vCard property with the specified name, or <c>null</c> if no such property exists.</returns>
    public IVCardProperty this[string name] => ContainsKey(name) ? AllOf(name).FirstOrDefault() : null;
}