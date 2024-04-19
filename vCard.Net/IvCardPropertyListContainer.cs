namespace vCard.Net;

/// <summary>
/// Represents an object that contains a list of vCard properties.
/// </summary>
public interface IvCardPropertyListContainer : IvCardObject
{
    /// <summary>
    /// Gets the list of vCard properties associated with this container.
    /// </summary>
    vCardPropertyList Properties { get; }
}