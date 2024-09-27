using System;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents a vCard data type.
/// </summary>
public interface IvCardDataType : IvCardParameterCollectionContainer, ICopyable, IServiceProvider
{
    /// <summary>
    /// Gets or sets the value type of the vCard data type.
    /// </summary>
    string ValueType { get; set; }

    /// <summary>
    /// Gets the value type of the vCard data type.
    /// </summary>
    /// <returns>The type of the value.</returns>
    Type GetValueType();

    /// <summary>
    /// Sets the value type of the vCard data type.
    /// </summary>
    /// <param name="type">The type of the value.</param>
    void SetValueType(string type);

    /// <summary>
    /// Gets or sets the vCard object associated with this data type.
    /// </summary>
    IvCardObject AssociatedObject { get; set; }

    /// <summary>
    /// Gets or sets the language of the data type.
    /// </summary>
    string Language { get; set; }
}