using System;

namespace vCard.Net.Collections;

/// <summary>
/// Provides data for an event that carries two objects.
/// </summary>
/// <typeparam name="T">The type of the first object.</typeparam>
/// <typeparam name="TU">The type of the second object.</typeparam>
public class ObjectEventArgs<T, TU> : EventArgs
{
    /// <summary>
    /// Gets or sets the first object.
    /// </summary>
    public T First { get; set; }

    /// <summary>
    /// Gets or sets the second object.
    /// </summary>
    public TU Second { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectEventArgs{T, TU}"/> class with the specified objects.
    /// </summary>
    /// <param name="first">The first object.</param>
    /// <param name="second">The second object.</param>
    public ObjectEventArgs(T first, TU second)
    {
        First = first;
        Second = second;
    }
}