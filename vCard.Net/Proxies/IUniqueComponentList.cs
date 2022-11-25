using System.Collections.Generic;
using vCard.Net.CardComponents;

namespace vCard.Net.Proxies
{
    public interface IUniqueComponentList<TComponentType> :
        ICardObjectList<TComponentType> where TComponentType : class, IUniqueComponent
    {
        TComponentType this[string uid] { get; set; }
        void AddRange(IEnumerable<TComponentType> collection);
    }
}