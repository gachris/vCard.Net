namespace vCard.Net.Collections;

/// <summary>
/// Represents a multi-linked list.
/// </summary>
/// <typeparam name="TType">The type of elements in the list.</typeparam>
public interface IMultiLinkedList<TType> : IList<TType>
{
    /// <summary>
    /// Gets the starting index of the linked list in a larger collection.
    /// </summary>
    int StartIndex { get; }

    /// <summary>
    /// Gets the exclusive ending index of the linked list in a larger collection.
    /// </summary>
    int ExclusiveEnd { get; }
}