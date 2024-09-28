using vCard.Net.Collections;

namespace vCard.Net;

/// <summary>
/// Represents a collection of vCard objects of a specific type.
/// </summary>
/// <typeparam name="TType">The type of vCard objects in the collection.</typeparam>
public interface IVCardObjectList<TType> : IGroupedCollection<string, TType> where TType : class, IVCardObject
{
    /// <summary>
    /// Gets the vCard object at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the vCard object to get.</param>
    /// <returns>The vCard object at the specified index.</returns>
    TType this[int index] { get; }
}
