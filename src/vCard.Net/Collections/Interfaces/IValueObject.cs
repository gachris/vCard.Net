namespace vCard.Net.Collections.Interfaces;

/// <summary>
/// Represents an object that holds one or more values of a specific type.
/// </summary>
/// <typeparam name="T">The type of values stored in the object.</typeparam>
public interface IValueObject<T>
{
    /// <summary>
    /// Gets the collection of values stored in the object.
    /// </summary>
    IEnumerable<T> Values { get; }

    /// <summary>
    /// Determines whether the specified value is contained within the object.
    /// </summary>
    /// <param name="value">The value to check for containment.</param>
    /// <returns><c>true</c> if the value is contained within the object; otherwise, <c>false</c>.</returns>
    bool ContainsValue(T value);

    /// <summary>
    /// Sets the value of the object to the specified value.
    /// </summary>
    /// <param name="value">The value to set.</param>
    void SetValue(T value);

    /// <summary>
    /// Sets the values of the object to the specified collection of values.
    /// </summary>
    /// <param name="values">The collection of values to set.</param>
    void SetValue(IEnumerable<T> values);

    /// <summary>
    /// Adds a value to the object.
    /// </summary>
    /// <param name="value">The value to add.</param>
    void AddValue(T value);

    /// <summary>
    /// Removes a value from the object.
    /// </summary>
    /// <param name="value">The value to remove.</param>
    void RemoveValue(T value);

    /// <summary>
    /// Gets the count of values stored in the object.
    /// </summary>
    int ValueCount { get; }
}