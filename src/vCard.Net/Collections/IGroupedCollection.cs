using System;
using System.Collections.Generic;

namespace vCard.Net.Collections;

/// <summary>
/// Represents a collection of items grouped by a common group key.
/// </summary>
/// <typeparam name="TGroup">The type of the group for grouping items.</typeparam>
/// <typeparam name="TItem">The type of items in the collection.</typeparam>
public interface IGroupedCollection<TGroup, TItem> : ICollection<TItem> where TItem : class, IGroupedObject<TGroup>
{
    /// <summary>
    /// Fired after an item is added to the collection.
    /// </summary>
    event EventHandler<ObjectEventArgs<TItem, int>> ItemAdded;

    /// <summary>
    /// Removes all items with the specified group from the collection.
    /// </summary>
    /// <param name="group">The group to remove items for.</param>
    /// <returns>True if items were removed, false otherwise.</returns>
    bool Remove(TGroup group);

    /// <summary>
    /// Clears all items matching the specified group from the collection.
    /// </summary>
    /// <param name="group">The group to clear items for.</param>
    void Clear(TGroup group);

    /// <summary>
    /// Returns true if the collection contains at least one item with a matching group, false otherwise.
    /// </summary>
    /// <param name="group">The group to check for.</param>
    /// <returns>True if the collection contains items with the specified group, false otherwise.</returns>
    bool ContainsKey(TGroup group);

    /// <summary>
    /// Returns the number of items in the collection with a matching group.
    /// </summary>
    /// <param name="group">The group to count items for.</param>
    /// <returns>The number of items with the specified group.</returns>
    int CountOf(TGroup group);

    /// <summary>
    /// Returns a sequence of items that match the specified group.
    /// </summary>
    /// <param name="group">The group to retrieve items for.</param>
    /// <returns>An enumerable sequence of items matching the group.</returns>
    IEnumerable<TItem> AllOf(TGroup group);
}