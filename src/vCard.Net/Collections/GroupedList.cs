using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace vCard.Net.Collections;

/// <summary>
/// Represents a list of objects that are grouped by a common key.
/// </summary>
/// <typeparam name="TGroup">The type of the key used for grouping.</typeparam>
/// <typeparam name="TItem">The type of the objects in the list.</typeparam>
public class GroupedList<TGroup, TItem> : IGroupedList<TGroup, TItem> where TItem : class, IGroupedObject<TGroup>
{
    private readonly List<IMultiLinkedList<TItem>> _lists = new List<IMultiLinkedList<TItem>>();
    private readonly Dictionary<TGroup, IMultiLinkedList<TItem>> _dictionary = new Dictionary<TGroup, IMultiLinkedList<TItem>>();

    private IMultiLinkedList<TItem> EnsureList(TGroup group)
    {
        if (group == null)
        {
            return null;
        }

        if (_dictionary.ContainsKey(group))
        {
            return _dictionary[group];
        }

        var list = new MultiLinkedList<TItem>();
        _dictionary[group] = list;

        _lists.Add(list);
        return list;
    }

    private IMultiLinkedList<TItem> ListForIndex(int index, out int relativeIndex)
    {
        foreach (var list in _lists.Where(list => list.StartIndex <= index && list.ExclusiveEnd > index))
        {
            relativeIndex = index - list.StartIndex;
            return list;
        }
        relativeIndex = -1;
        return null;
    }

    /// <inheritdoc/>
    public event EventHandler<ObjectEventArgs<TItem, int>> ItemAdded;

    /// <summary>
    /// Invokes the ItemAdded event.
    /// </summary>
    /// <param name="item">The item that was added.</param>
    /// <param name="index">The index at which the item was added.</param>
    protected void OnItemAdded(TItem item, int index) => ItemAdded?.Invoke(this, new ObjectEventArgs<TItem, int>(item, index));

    /// <inheritdoc/>
    public virtual void Add(TItem item)
    {
        if (item == null)
        {
            return;
        }

        // Add a new list if necessary
        var group = item.Group;
        var list = EnsureList(group);
        var index = list.Count;
        list.Add(item);
        OnItemAdded(item, list.StartIndex + index);
    }

    /// <inheritdoc/>
    public virtual int IndexOf(TItem item)
    {
        var group = item.Group;
        if (!_dictionary.ContainsKey(group))
        {
            return -1;
        }

        // Get the list associated with this object's group
        var list = _dictionary[group];

        // Find the object within the list.
        var index = list.IndexOf(item);

        // Return the index within the overall KeyedList
        return index >= 0 ? list.StartIndex + index : -1;
    }

    /// <inheritdoc/>
    public virtual void Clear(TGroup group)
    {
        if (!_dictionary.ContainsKey(group))
        {
            return;
        }

        // Clear the list (note that this also clears the list in the _Lists object).
        _dictionary[group].Clear();
    }

    /// <inheritdoc/>
    public virtual void Clear()
    {
        _dictionary.Clear();
        _lists.Clear();
    }

    /// <inheritdoc/>
    public virtual bool ContainsKey(TGroup group) => _dictionary.ContainsKey(@group);

    /// <inheritdoc/>
    public virtual int Count => _lists.Sum(list => list.Count);

    /// <inheritdoc/>
    public virtual int CountOf(TGroup group) => _dictionary.ContainsKey(group) ? _dictionary[group].Count : 0;

    /// <inheritdoc/>
    public virtual IEnumerable<TItem> Values() => _dictionary.Values.SelectMany(i => i);

    /// <inheritdoc/>
    public virtual IEnumerable<TItem> AllOf(TGroup group)
    {
        return _dictionary.ContainsKey(@group) ? _dictionary[@group] : Array.Empty<TItem>();
    }

    /// <inheritdoc/>
    public virtual bool Remove(TItem obj)
    {
        var group = obj.Group;
        if (!_dictionary.ContainsKey(group))
        {
            return false;
        }

        var items = _dictionary[group];
        var index = items.IndexOf(obj);

        if (index < 0)
        {
            return false;
        }

        items.RemoveAt(index);
        return true;
    }

    /// <inheritdoc/>
    public virtual bool Remove(TGroup group)
    {
        if (!_dictionary.ContainsKey(group))
        {
            return false;
        }

        var list = _dictionary[group];
        for (var i = list.Count - 1; i >= 0; i--)
        {
            list.RemoveAt(i);
        }
        return true;
    }

    /// <inheritdoc/>
    public virtual bool Contains(TItem item)
    {
        var group = item.Group;
        return _dictionary.ContainsKey(group) && _dictionary[group].Contains(item);
    }

    /// <inheritdoc/>
    public virtual void CopyTo(TItem[] array, int arrayIndex) => _dictionary.SelectMany(kvp => kvp.Value).ToArray().CopyTo(array, arrayIndex);

    /// <inheritdoc/>
    public virtual bool IsReadOnly => false;

    /// <inheritdoc/>
    public virtual void Insert(int index, TItem item)
    {
        var list = ListForIndex(index, out int relativeIndex);
        if (list == null)
        {
            return;
        }

        list.Insert(relativeIndex, item);
        OnItemAdded(item, index);
    }

    /// <inheritdoc/>
    public virtual void RemoveAt(int index)
    {
        var list = ListForIndex(index, out int relativeIndex);
        if (list == null)
        {
            return;
        }
        var item = list[relativeIndex];
        list.RemoveAt(relativeIndex);
    }

    /// <inheritdoc/>
    public virtual TItem this[int index]
    {
        get
        {
            var list = ListForIndex(index, out int relativeIndex);
            return list?[relativeIndex];
        }
        set
        {
            var list = ListForIndex(index, out int relativeIndex);
            if (list == null)
            {
                return;
            }

            // Remove the item at that index and replace it
            var item = list[relativeIndex];
            list.RemoveAt(relativeIndex);
            list.Insert(relativeIndex, value);
            OnItemAdded(item, index);
        }
    }

    /// <inheritdoc/>
    public IEnumerator<TItem> GetEnumerator() => new GroupedListEnumerator<TItem>(_lists);

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => new GroupedListEnumerator<TItem>(_lists);
}