using System.Linq;
using vCard.Net.Collections;
using vCard.Net.Collections.Proxies;

namespace vCard.Net.Proxies
{
    public class CardObjectListProxy<TType> : GroupedCollectionProxy<string, ICardObject, TType>, ICardObjectList<TType>
        where TType : class, ICardObject
    {
        public CardObjectListProxy(IGroupedCollection<string, ICardObject> list) : base(list) { }

        public virtual TType this[int index] => this.Skip(index).FirstOrDefault();
    }
}