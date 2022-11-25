using vCard.Net.Collections;

namespace vCard.Net
{
    /// <summary>
    /// A collection of vCard objects.
    /// </summary>
    public class CardObjectList : GroupedList<string, ICardObject>, ICardObjectList<ICardObject>
    {
        public CardObjectList(ICardObject parent) { }
    }
}