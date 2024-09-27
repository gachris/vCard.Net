using vCard.Net.Collections;

namespace vCard.Net;

/// <summary>
/// Represents a collection of parameters associated with a vCard component or property.
/// </summary>
public interface IParameterCollection : IGroupedList<string, vCardParameter>
{
    /// <summary>
    /// Sets the parent vCard object for the parameter collection.
    /// </summary>
    /// <param name="parent">The parent vCard object.</param>
    void SetParent(IvCardObject parent);

    /// <summary>
    /// Adds a parameter with the specified name and value to the collection.
    /// </summary>
    /// <param name="name">The name of the parameter.</param>
    /// <param name="value">The value of the parameter.</param>
    void Add(string name, string value);

    /// <summary>
    /// Gets the value of the parameter with the specified name.
    /// </summary>
    /// <param name="name">The name of the parameter.</param>
    /// <returns>The value of the parameter, or <c>null</c> if not found.</returns>
    string Get(string name);

    /// <summary>
    /// Gets a list of values for the parameter with the specified name.
    /// </summary>
    /// <param name="name">The name of the parameter.</param>
    /// <returns>A list of values for the parameter, or an empty list if not found.</returns>
    IList<string> GetMany(string name);

    /// <summary>
    /// Sets the value of the parameter with the specified name.
    /// If the parameter does not exist, it will be added to the collection.
    /// </summary>
    /// <param name="name">The name of the parameter.</param>
    /// <param name="value">The value to set.</param>
    void Set(string name, string value);

    /// <summary>
    /// Sets the values of the parameter with the specified name.
    /// If the parameter does not exist, it will be added to the collection.
    /// </summary>
    /// <param name="name">The name of the parameter.</param>
    /// <param name="values">The values to set.</param>
    void Set(string name, IEnumerable<string> values);
}