using vCard.Net.CardComponents;

namespace vCard.Net.Serialization;

/// <summary>
/// Factory class for creating vCard components.
/// </summary>
public class VCardComponentFactory
{
    /// <summary>
    /// Builds a vCard component based on the specified object name.
    /// </summary>
    /// <param name="objectName">The name of the vCard component.</param>
    /// <returns>An instance of <see cref="IVCardComponent"/>.</returns>
    public virtual IVCardComponent Build(string objectName)
    {
        var name = objectName.ToUpper();
        IVCardComponent c = name switch
        {
            Components.VCARD => new VCard()
            {
                Name = name
            },
            "AGENT" => new Agent()
            {
                Name = Components.VCARD
            },
            _ => new VCardComponent()
            {
                Name = name
            }
        };
        return c;
    }
}