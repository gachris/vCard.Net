using vCard.Net.DataTypes;

namespace vCard.Net.CardComponents;

/// <summary>
/// Represents a component within a vCard object.
/// </summary>
public interface IvCardComponent : IvCardPropertyListContainer
{
    /// <summary>
    /// Gets or sets the version of the vCard.
    /// </summary>
    vCardVersion Version { get; set; }
}