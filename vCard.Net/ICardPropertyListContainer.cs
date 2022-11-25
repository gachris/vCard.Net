namespace vCard.Net
{
    public interface ICardPropertyListContainer : ICardObject
    {
        CardPropertyList Properties { get; }
    }
}