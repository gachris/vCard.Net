using System.Collections.Generic;

namespace vCard.Net.Collections;

/// <summary>
/// Represents a multi-linked list implementation.
/// </summary>
/// <typeparam name="TType">The type of elements in the list.</typeparam>
public class MultiLinkedList<TType> : List<TType>, IMultiLinkedList<TType>
{
    private IMultiLinkedList<TType> _previous;
    private IMultiLinkedList<TType> _next;

    /// <summary>
    /// Sets the previous node in the linked list.
    /// </summary>
    /// <param name="previous">The previous node.</param>
    public virtual void SetPrevious(IMultiLinkedList<TType> previous) => _previous = previous;

    /// <summary>
    /// Sets the next node in the linked list.
    /// </summary>
    /// <param name="next">The next node.</param>
    public virtual void SetNext(IMultiLinkedList<TType> next) => _next = next;

    /// <summary>
    /// Gets the starting index of this segment in the overall list.
    /// </summary>
    public virtual int StartIndex => _previous?.ExclusiveEnd ?? 0;

    /// <summary>
    /// Gets the exclusive end index of this segment in the overall list.
    /// </summary>
    public virtual int ExclusiveEnd => Count > 0 ? StartIndex + Count : StartIndex;
}
