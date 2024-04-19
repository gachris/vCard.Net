namespace vCard.Net.CardComponents;

/// <summary>
/// Represents a unique component of a vCard.
/// </summary>
public interface IUniqueComponent : IvCardComponent
{
    /// <summary>
    /// Gets or sets the unique identifier (UID) of the component.
    /// </summary>
    string Uid { get; set; }
}