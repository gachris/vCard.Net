using vCard.Net.Collections;
using vCard.Net.Collections.Proxies;

namespace vCard.Net.Proxies;

/// <summary>
/// Represents a proxy class for a list of vCard objects.
/// </summary>
/// <typeparam name="TType">The type of vCard objects.</typeparam>
public class VCardObjectListProxy<TType> : GroupedCollectionProxy<string, IVCardObject, TType>, IVCardObjectList<TType>
    where TType : class, IVCardObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VCardObjectListProxy{TType}"/> class.
    /// </summary>
    /// <param name="list">The underlying grouped collection of vCard objects.</param>
    public VCardObjectListProxy(IGroupedCollection<string, IVCardObject> list) : base(list) { }

    /// <inheritdoc/>
    public virtual TType this[int index] => this.Skip(index).FirstOrDefault();
}