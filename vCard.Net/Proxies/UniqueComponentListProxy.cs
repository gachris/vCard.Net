using System;
using System.Collections.Generic;
using System.Linq;
using vCard.Net.CardComponents;
using vCard.Net.Collections;

namespace vCard.Net.Proxies
{
    public class UniqueComponentListProxy<TComponentType> :
        CardObjectListProxy<TComponentType>,
        IUniqueComponentList<TComponentType>
        where TComponentType : class, IUniqueComponent
    {
        private readonly Dictionary<string, TComponentType> _lookup;

        public UniqueComponentListProxy(IGroupedCollection<string, ICardObject> children) : base(children) => _lookup = new Dictionary<string, TComponentType>();

        private TComponentType Search(string uid)
        {
            if (_lookup.TryGetValue(uid, out var componentType))
            {
                return componentType;
            }

            var item = this.FirstOrDefault(c => string.Equals(c.Uid, uid, StringComparison.OrdinalIgnoreCase));

            if (item == null)
            {
                return default;
            }

            _lookup[uid] = item;
            return item;
        }

        public virtual TComponentType this[string uid]
        {
            get => Search(uid);
            set
            {
                // Find the item matching the UID
                var item = Search(uid);

                if (item != null)
                {
                    Remove(item);
                }

                if (value != null)
                {
                    Add(value);
                }
            }
        }

        public void AddRange(IEnumerable<TComponentType> collection)
        {
            foreach (var element in collection)
            {
                Add(element);
            }
        }
    }
}