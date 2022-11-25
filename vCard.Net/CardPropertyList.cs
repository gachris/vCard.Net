using System.Linq;
using vCard.Net.Collections;

namespace vCard.Net
{
    public class CardPropertyList : GroupedValueList<string, ICardProperty, CardProperty, object>
    {
        private readonly ICardObject _mParent;

        public CardPropertyList() { }

        public CardPropertyList(ICardObject parent)
        {
            _mParent = parent;
            ItemAdded += vCardPropertyList_ItemAdded;
        }

        private void vCardPropertyList_ItemAdded(object sender, ObjectEventArgs<ICardProperty, int> e) => e.First.Parent = _mParent;

        public ICardProperty this[string name] => ContainsKey(name)
            ? AllOf(name).FirstOrDefault()
            : null;
    }
}