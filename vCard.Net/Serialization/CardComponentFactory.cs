using vCard.Net.CardComponents;

namespace vCard.Net.Serialization
{
    public class CardComponentFactory
    {
        public virtual ICardComponent Build(string objectName)
        {
            var name = objectName.ToUpper();
            ICardComponent c;
            switch (name)
            {
                case Components.VCARD:
                    c = new Card();
                    break;
                default:
                    c = new CardComponent();
                    break;
            }
            c.Name = name;
            return c;
        }
    }
}