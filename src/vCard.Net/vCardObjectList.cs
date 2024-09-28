using vCard.Net.Collections;

namespace vCard.Net;

/// <summary>
/// Represents a collection of vCard objects.
/// </summary>
public class VCardObjectList : GroupedList<string, IVCardObject>, IVCardObjectList<IVCardObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VCardObjectList"/> class with the specified parent vCard object.
    /// </summary>
    /// <param name="parent">The parent vCard object to which this list belongs.</param>
    public VCardObjectList(IVCardObject parent)
    {
    }
}