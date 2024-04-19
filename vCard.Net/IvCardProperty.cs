using vCard.Net.Collections.Interfaces;
using vCard.Net.DataTypes;

namespace vCard.Net;

/// <summary>
/// Represents a property of a vCard, which can hold a single value.
/// </summary>
public interface IvCardProperty : IvCardParameterCollectionContainer, IvCardObject, IValueObject<object>
{
    /// <summary>
    /// Gets or sets the value of the property.
    /// </summary>
    object Value { get; set; }
}