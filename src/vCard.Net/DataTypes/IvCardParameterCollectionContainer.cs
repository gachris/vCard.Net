namespace vCard.Net.DataTypes;

/// <summary>
/// Represents a container for vCard parameters.
/// </summary>
public interface IvCardParameterCollectionContainer
{
    /// <summary>
    /// Gets the collection of parameters associated with the vCard data type.
    /// </summary>
    IParameterCollection Parameters { get; }
}
