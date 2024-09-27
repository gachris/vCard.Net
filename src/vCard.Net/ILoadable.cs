using System;

namespace vCard.Net;

/// <summary>
/// Provides functionality for tracking the loading status of an object.
/// </summary>
public interface ILoadable
{
    /// <summary>
    /// Gets whether or not the object has been loaded.
    /// </summary>
    bool IsLoaded { get; }

    /// <summary>
    /// An event that fires when the object has been loaded.
    /// </summary>
    event EventHandler Loaded;

    /// <summary>
    /// Fires the <see cref="Loaded"/> event.
    /// </summary>
    void OnLoaded();
}