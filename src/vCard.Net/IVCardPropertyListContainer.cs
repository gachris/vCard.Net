namespace vCard.Net;

/// <summary>
/// Represents an object that contains a list of vCard properties.
/// </summary>
public interface IVCardPropertyListContainer : IVCardObject
{
    /// <summary>
    /// Gets the list of vCard properties associated with this container.
    /// </summary>
    VCardPropertyList Properties { get; }
}