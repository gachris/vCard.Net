using vCard.Net.DataTypes;

namespace vCard.Net;

/// <summary>
/// Provides functionality for copying relevant fields/properties from one object to another.
/// </summary>
public interface ICopyable
{
    /// <summary>
    /// Copies all relevant fields/properties from
    /// the target object to the current one.
    /// </summary>
    void CopyFrom(ICopyable obj);

    /// <summary>
    /// Returns a deep copy of the current object. For the most part, this is only necessary when working with mutable reference types,
    /// (i.e. <see cref="vCardDateTime"/>). For most other types, it's unnecessary overhead. The pattern that identifies whether it's necessary to copy
    /// or not is whether arithmetic operations mutate fields or properties. <see cref="vCardDateTime"/> is a good example where + and - would otherwise
    /// change the Value of the underlying DateTime.
    /// </summary>
    T Copy<T>();
}