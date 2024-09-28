using System.Collections;
using vCard.Net.Collections.Interfaces;

namespace vCard.Net.Collections.Proxies;

/// <summary>
/// A proxy for a keyed list, providing access to values of type <typeparamref name="TNewValue"/>.
/// </summary>
/// <typeparam name="TGroup">The type of the key for grouping.</typeparam>
/// <typeparam name="TInterface">The interface type of the original grouped object.</typeparam>
/// <typeparam name="TItem">The concrete type of the original grouped object.</typeparam>
/// <typeparam name="TOriginalValue">The original value type stored in the grouped object.</typeparam>
/// <typeparam name="TNewValue">The new value type provided by the proxy.</typeparam>
public class GroupedValueListProxy<TGroup, TInterface, TItem, TOriginalValue, TNewValue> : IList<TNewValue>
    where TInterface : class, IGroupedObject<TGroup>, IValueObject<TOriginalValue>
    where TItem : new()
{
    private readonly GroupedValueList<TGroup, TInterface, TItem, TOriginalValue> _realObject;
    private readonly TGroup _group;
    private TInterface _container;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroupedValueListProxy{TGroup, TInterface, TItem, TOriginalValue, TNewValue}"/> class.
    /// </summary>
    /// <param name="realObject">The original grouped value list.</param>
    /// <param name="group">The group key.</param>
    public GroupedValueListProxy(GroupedValueList<TGroup, TInterface, TItem, TOriginalValue> realObject, TGroup group)
    {
        _realObject = realObject;
        _group = group;
    }

    private TInterface EnsureContainer()
    {
        if (_container != null)
        {
            return _container;
        }

        // Find an item that matches our group
        _container = Items.FirstOrDefault();

        // If no item is found, create a new object and add it to the list
        if (!Equals(_container, default(TInterface)))
        {
            return _container;
        }
        var container = new TItem();
        if (container is not TInterface)
        {
            throw new Exception("Could not create a container for the value - the container is not of type " + typeof(TInterface).Name);
        }

        _container = (TInterface)(object)container;
        _container.Group = _group;
        _realObject.Add(_container);
        return _container;
    }

    private void IterateValues(Func<IValueObject<TOriginalValue>, int, int, bool> action)
    {
        var i = 0;
        foreach (var obj in _realObject)
        {
            // Get the number of items of the target value i this object
            var count = obj.Values?.OfType<TNewValue>().Count() ?? 0;

            // Perform some action on this item
            if (!action(obj, i, count))
                return;

            i += count;
        }
    }

    private IEnumerator<TNewValue> GetEnumeratorInternal() => Items
            .Where(o => o.ValueCount > 0)
            .SelectMany(o => o.Values.OfType<TNewValue>())
            .GetEnumerator();

    /// <inheritdoc/>
    public virtual void Add(TNewValue item)
    {
        // Add the value to the object
        if (item is TOriginalValue)
        {
            var value = (TOriginalValue)(object)item;
            EnsureContainer().AddValue(value);
        }
    }

    /// <inheritdoc/>
    public virtual void Clear()
    {
        var items = Items.Where(o => o.Values != null);

        foreach (var original in items)
        {
            // Clear all values from each matching object
            original.SetValue(default(TOriginalValue));
        }
    }

    /// <inheritdoc/>
    public virtual bool Contains(TNewValue item) => Items.Any(o => o.ContainsValue((TOriginalValue)(object)item));

    /// <inheritdoc/>
    public virtual void CopyTo(TNewValue[] array, int arrayIndex) => Items
            .Where(o => o.Values != null)
            .SelectMany(o => o.Values)
            .ToArray()
            .CopyTo(array, arrayIndex);

    /// <inheritdoc/>
    public virtual int Count => Items.Sum(o => o.ValueCount);

    /// <inheritdoc/>
    public virtual bool IsReadOnly => false;

    /// <inheritdoc/>
    public virtual bool Remove(TNewValue item)
    {
        if (item is not TOriginalValue)
        {
            return false;
        }

        var value = (TOriginalValue)(object)item;
        var container = Items.FirstOrDefault(o => o.ContainsValue(value));

        if (container == null)
        {
            return false;
        }

        container.RemoveValue(value);
        return true;
    }

    /// <inheritdoc/>
    public virtual IEnumerator<TNewValue> GetEnumerator() => GetEnumeratorInternal();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumeratorInternal();

    /// <inheritdoc/>
    public virtual int IndexOf(TNewValue item)
    {
        var index = -1;

        if (item is not TOriginalValue)
        {
            return index;
        }

        var value = (TOriginalValue)(object)item;
        IterateValues((o, i, count) =>
        {
            if (o.Values != null && o.Values.Contains(value))
            {
                var list = o.Values.ToList();
                index = i + list.IndexOf(value);
                return false;
            }
            return true;
        });

        return index;
    }

    /// <inheritdoc/>
    public virtual void Insert(int index, TNewValue item)
    {
        IterateValues((o, i, count) =>
        {
            var value = (TOriginalValue)(object)item;

            // Determine if this index is found within this object
            if (index < i || index >= count)
            {
                return true;
            }

            // Convert the items to a list
            var items = o.Values.ToList();
            // Insert the item at the relative index within the list
            items.Insert(index - i, value);
            // Set the new list
            o.SetValue(items);
            return false;
        });
    }

    /// <inheritdoc/>
    public virtual void RemoveAt(int index)
    {
        IterateValues((o, i, count) =>
        {
            // Determine if this index is found within this object
            if (index >= i && index < count)
            {
                // Convert the items to a list
                var items = o.Values.ToList();
                // Remove the item at the relative index within the list
                items.RemoveAt(index - i);
                // Set the new list
                o.SetValue(items);
                return false;
            }
            return true;
        });
    }

    /// <inheritdoc/>
    public virtual TNewValue this[int index]
    {
        get
        {
            return index >= 0 && index < Count
                ? Items
                    .SelectMany(i => i.Values?.OfType<TNewValue>())
                    .Skip(index)
                    .FirstOrDefault()
                : default;
        }
        set
        {
            if (index >= 0 && index < Count)
            {
                if (!Equals(value, default(TNewValue)))
                {
                    Insert(index, value);
                    index++;
                }
                RemoveAt(index);
            }
        }
    }

    /// <summary>
    /// Gets the items in the grouped value list, optionally filtered by group.
    /// </summary>
    public virtual IEnumerable<TInterface> Items => _group == null ? _realObject : _realObject.AllOf(_group);
}