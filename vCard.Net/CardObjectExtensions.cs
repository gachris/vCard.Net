namespace vCard.Net
{
    public static class CardObjectExtensions
    {
        public static void AddChild<TItem>(this ICardObject obj, TItem child) where TItem : ICardObject => obj.Children.Add(child);

        public static void RemoveChild<TItem>(this ICardObject obj, TItem child) where TItem : ICardObject => obj.Children.Remove(child);
    }
}