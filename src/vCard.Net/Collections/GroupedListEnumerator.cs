using System.Collections;

namespace vCard.Net.Collections;

/// <summary>
/// Represents an enumerator for iterating through a collection of multi-linked lists within a <see cref="GroupedList{TGroup, TItem}"/>.
/// </summary>
/// <typeparam name="TType">The type of elements in the grouped list.</typeparam>
public class GroupedListEnumerator<TType> : IEnumerator<TType>
{
    private readonly IList<IMultiLinkedList<TType>> _lists;
    private IEnumerator<IMultiLinkedList<TType>> _listsEnumerator;
    private IEnumerator<TType> _listEnumerator;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroupedListEnumerator{TType}"/> class with the specified list of multi-linked lists.
    /// </summary>
    /// <param name="lists">The list of multi-linked lists to enumerate.</param>
    public GroupedListEnumerator(IList<IMultiLinkedList<TType>> lists) => _lists = lists;

    /// <inheritdoc/>
    public virtual TType Current
        => _listEnumerator == null
            ? default
            : _listEnumerator.Current;

    /// <inheritdoc/>
    public virtual void Dispose() => Reset();

    private void DisposeListEnumerator()
    {
        if (_listEnumerator == null)
        {
            return;
        }
        _listEnumerator.Dispose();
        _listEnumerator = null;
    }

    object IEnumerator.Current
        => _listEnumerator == null
            ? default
            : _listEnumerator.Current;

    private bool MoveNextList()
    {
        if (_listsEnumerator == null)
        {
            _listsEnumerator = _lists.GetEnumerator();
        }

        if (_listsEnumerator == null)
        {
            return false;
        }

        if (!_listsEnumerator.MoveNext())
        {
            return false;
        }

        DisposeListEnumerator();
        if (_listsEnumerator.Current == null)
        {
            return false;
        }

        _listEnumerator = _listsEnumerator.Current.GetEnumerator();
        return true;
    }

    /// <inheritdoc/>
    public virtual bool MoveNext()
    {
        while (true)
        {
            if (_listEnumerator == null)
            {
                if (MoveNextList())
                {
                    continue;
                }
            }
            else
            {
                if (_listEnumerator.MoveNext())
                {
                    return true;
                }
                DisposeListEnumerator();
                if (MoveNextList())
                {
                    continue;
                }
            }
            return false;
        }
    }

    /// <inheritdoc/>
    public virtual void Reset()
    {
        if (_listsEnumerator == null)
        {
            return;
        }

        _listsEnumerator.Dispose();
        _listsEnumerator = null;
    }
}