using System;
using System.Collections.Generic;
using System.Linq;
using vCard.Net.CardComponents;
using vCard.Net.Collections;

namespace vCard.Net.Proxies;

/// <summary>
/// Represents a proxy class for a list of unique components.
/// </summary>
/// <typeparam name="TComponentType">The type of the component.</typeparam>
public class UniqueComponentListProxy<TComponentType> :
    vCardObjectListProxy<TComponentType>,
    IUniqueComponentList<TComponentType>
    where TComponentType : class, IUniqueComponent
{
    private readonly Dictionary<string, TComponentType> _lookup;

    /// <summary>
    /// Initializes a new instance of the <see cref="UniqueComponentListProxy{TComponentType}"/> class.
    /// </summary>
    /// <param name="children">The underlying grouped collection of card objects.</param>
    public UniqueComponentListProxy(IGroupedCollection<string, IvCardObject> children) : base(children) => _lookup = new Dictionary<string, TComponentType>();

    private TComponentType Search(string uid)
    {
        if (_lookup.TryGetValue(uid, out var componentType))
        {
            return componentType;
        }

        var item = this.FirstOrDefault(c => string.Equals(c.Uid, uid, StringComparison.OrdinalIgnoreCase));

        if (item == null)
        {
            return default;
        }

        _lookup[uid] = item;
        return item;
    }

    /// <inheritdoc/>
    public virtual TComponentType this[string uid]
    {
        get => Search(uid);
        set
        {
            // Find the item matching the UID
            var item = Search(uid);

            if (item != null)
            {
                Remove(item);
            }

            if (value != null)
            {
                Add(value);
            }
        }
    }

    /// <inheritdoc/>
    public void AddRange(IEnumerable<TComponentType> collection)
    {
        foreach (var element in collection)
        {
            Add(element);
        }
    }
}