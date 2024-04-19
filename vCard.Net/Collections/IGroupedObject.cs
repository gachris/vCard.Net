namespace vCard.Net.Collections;

/// <summary>
/// Represents an object that belongs to a group.
/// </summary>
/// <typeparam name="TGroup">The type of the group.</typeparam>
public interface IGroupedObject<TGroup>
{
    /// <summary>
    /// Gets or sets the group to which the object belongs.
    /// </summary>
    TGroup Group { get; set; }
}