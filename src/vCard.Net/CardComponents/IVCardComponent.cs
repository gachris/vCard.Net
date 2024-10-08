﻿using vCard.Net.DataTypes;

namespace vCard.Net.CardComponents;

/// <summary>
/// Represents a component within a vCard object.
/// </summary>
public interface IVCardComponent : IVCardPropertyListContainer
{
    /// <summary>
    /// Gets or sets the version of the vCard.
    /// </summary>
    VCardVersion Version { get; set; }
}