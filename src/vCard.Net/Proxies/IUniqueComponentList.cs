using vCard.Net.CardComponents;

namespace vCard.Net.Proxies;

/// <summary>
/// Represents a collection of unique components of the specified type.
/// </summary>
/// <typeparam name="TComponentType">The type of unique components in the list.</typeparam>
public interface IUniqueComponentList<TComponentType> :
    IVCardObjectList<TComponentType> where TComponentType : class, IUniqueComponent
{
    /// <summary>
    /// Gets or sets the unique component with the specified UID.
    /// </summary>
    /// <param name="uid">The UID of the unique component to get or set.</param>
    /// <returns>The unique component with the specified UID.</returns>
    TComponentType this[string uid] { get; set; }

    /// <summary>
    /// Adds a range of unique components to the list.
    /// </summary>
    /// <param name="collection">The collection of unique components to add.</param>
    void AddRange(IEnumerable<TComponentType> collection);
}