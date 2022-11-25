using System.Collections.Generic;

namespace vCard.Net.Collections
{
    public interface IMultiLinkedList<TType> :
        IList<TType>
    {
        int StartIndex { get; }
        int ExclusiveEnd { get; }
    }
}