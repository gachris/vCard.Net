namespace vCard.Net;

/// <summary>
/// Provides extension methods for working with vCard objects.
/// </summary>
public static class vCardObjectExtensions
{
    /// <summary>
    /// Adds a child object to the parent vCard object.
    /// </summary>
    /// <typeparam name="TItem">The type of the child object.</typeparam>
    /// <param name="obj">The parent vCard object.</param>
    /// <param name="child">The child object to add.</param>
    public static void AddChild<TItem>(this IvCardObject obj, TItem child) where TItem : IvCardObject => obj.Children.Add(child);

    /// <summary>
    /// Removes a child object from the parent vCard object.
    /// </summary>
    /// <typeparam name="TItem">The type of the child object.</typeparam>
    /// <param name="obj">The parent vCard object.</param>
    /// <param name="child">The child object to remove.</param>
    public static void RemoveChild<TItem>(this IvCardObject obj, TItem child) where TItem : IvCardObject => obj.Children.Remove(child);
}