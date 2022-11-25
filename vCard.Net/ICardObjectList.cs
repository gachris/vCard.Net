using vCard.Net.Collections;

namespace vCard.Net
{
    public interface ICardObjectList<TType> : IGroupedCollection<string, TType> where TType : class, ICardObject
    {
        TType this[int index] { get; }
    }
}