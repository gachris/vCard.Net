﻿using vCard.Net.CardComponents;

namespace vCard.Net.Serialization;

/// <summary>
/// Factory class for creating vCard components.
/// </summary>
public class vCardComponentFactory
{
    /// <summary>
    /// Builds a vCard component based on the specified object name.
    /// </summary>
    /// <param name="objectName">The name of the vCard component.</param>
    /// <returns>An instance of <see cref="IvCardComponent"/>.</returns>
    public virtual IvCardComponent Build(string objectName)
    {
        var name = objectName.ToUpper();
        IvCardComponent c = name switch
        {
            Components.VCARD => new CardComponents.vCard(),
            _ => new vCardComponent(),
        };
        c.Name = name;
        return c;
    }
}