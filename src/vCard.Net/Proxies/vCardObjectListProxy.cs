using vCard.Net.Collections;
using vCard.Net.Collections.Proxies;

namespace vCard.Net.Proxies;

/// <summary>
/// Represents a proxy class for a list of vCard objects.
/// </summary>
/// <typeparam name="TType">The type of vCard objects.</typeparam>
public class vCardObjectListProxy<TType> : GroupedCollectionProxy<string, IvCardObject, TType>, IvCardObjectList<TType>
    where TType : class, IvCardObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="vCardObjectListProxy{TType}"/> class.
    /// </summary>
    /// <param name="list">The underlying grouped collection of vCard objects.</param>
    public vCardObjectListProxy(IGroupedCollection<string, IvCardObject> list) : base(list) { }

    /// <inheritdoc/>
    public virtual TType this[int index] => this.Skip(index).FirstOrDefault();
}