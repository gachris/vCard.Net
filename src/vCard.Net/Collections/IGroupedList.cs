namespace vCard.Net.Collections;

/// <summary>
/// Represents a grouped list of items, providing both collection and list functionalities.
/// </summary>
/// <typeparam name="TGroup">The type of the group for grouping items.</typeparam>
/// <typeparam name="TItem">The type of items in the list.</typeparam>
public interface IGroupedList<TGroup, TItem> : IGroupedCollection<TGroup, TItem>, IList<TItem> where TItem : class, IGroupedObject<TGroup>
{
    /// <summary>
    /// Returns the index of the given item within the list, or -1 if the item is not found.
    /// </summary>
    /// <param name="obj">The item to search for.</param>
    /// <returns>The index of the item, or -1 if not found.</returns>
    new int IndexOf(TItem obj);

    /// <summary>
    /// Gets the item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item to get.</param>
    /// <returns>The item at the specified index.</returns>
    new TItem this[int index] { get; }
}