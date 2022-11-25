namespace vCard.Net.Collections
{
    public interface IGroupedObject<TGroup>
    {
        TGroup Group { get; set; }
    }
}