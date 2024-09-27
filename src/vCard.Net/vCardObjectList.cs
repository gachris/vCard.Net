using vCard.Net.Collections;

namespace vCard.Net;

/// <summary>
/// Represents a collection of vCard objects.
/// </summary>
public class vCardObjectList : GroupedList<string, IvCardObject>, IvCardObjectList<IvCardObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="vCardObjectList"/> class with the specified parent vCard object.
    /// </summary>
    /// <param name="parent">The parent vCard object to which this list belongs.</param>
    public vCardObjectList(IvCardObject parent)
    {
    }
}