namespace vCard.Net.CardComponents
{
    public interface IUniqueComponent : ICardComponent
    {
        string Uid { get; set; }
    }
}