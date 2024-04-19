using System;

namespace vCard.Net;

/// <summary>
/// Represents a service provider interface for accessing and managing services.
/// </summary>
public interface IServiceProvider
{
    /// <summary>
    /// Gets the service with the specified name.
    /// </summary>
    /// <param name="name">The name of the service.</param>
    /// <returns>The service object, or <c>null</c> if not found.</returns>
    object GetService(string name);

    /// <summary>
    /// Gets the service of the specified type.
    /// </summary>
    /// <param name="type">The type of the service.</param>
    /// <returns>The service object, or <c>null</c> if not found.</returns>
    object GetService(Type type);

    /// <summary>
    /// Gets the service of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the service.</typeparam>
    /// <returns>The service object, or <c>null</c> if not found.</returns>
    T GetService<T>();

    /// <summary>
    /// Gets the service of the specified type with the specified name.
    /// </summary>
    /// <typeparam name="T">The type of the service.</typeparam>
    /// <param name="name">The name of the service.</param>
    /// <returns>The service object, or <c>null</c> if not found.</returns>
    T GetService<T>(string name);

    /// <summary>
    /// Sets the service with the specified name.
    /// </summary>
    /// <param name="name">The name of the service.</param>
    /// <param name="obj">The service object to set.</param>
    void SetService(string name, object obj);

    /// <summary>
    /// Sets the service of the specified type.
    /// </summary>
    /// <param name="obj">The service object to set.</param>
    void SetService(object obj);

    /// <summary>
    /// Removes the service of the specified type.
    /// </summary>
    /// <param name="type">The type of the service to remove.</param>
    void RemoveService(Type type);

    /// <summary>
    /// Removes the service with the specified name.
    /// </summary>
    /// <param name="name">The name of the service to remove.</param>
    void RemoveService(string name);
}