using System.Collections.Generic;
using vCard.Net.DataTypes;

namespace vCard.Net.CardComponents;

/// <summary>
/// Represents a vCard object, which is a standardized format for electronic business cards.
/// </summary>
/// <remarks>
/// The vCard format allows the exchange of contact information such as names, addresses, phone numbers,
/// email addresses, URLs, logos, photographs, and other information about individuals and organizations.
/// This class provides properties to represent various components of a vCard, including general properties,
/// identification properties, delivery addressing properties, communications properties, geographical properties,
/// organizational properties, explanatory properties, security properties, and calendar properties.
/// </remarks>
public interface IvCard
{
    #region General Properties

    /// <summary>
    /// Gets or sets the source of the vCard.
    /// </summary>
    Source Source { get; set; }

    /// <summary>
    /// Gets or sets the kind of the vCard.
    /// </summary>
    Kind Kind { get; set; }

    #endregion

    #region Identification Properties

    /// <summary>
    /// Gets or sets the name of the vCard.
    /// </summary>
    Name N { get; set; }

    /// <summary>
    /// Gets or sets the formatted name of the vCard.
    /// </summary>
    /// <remarks>
    /// This property allows the name of the vCard to be written in a manner specific to his or her culture.
    /// The formatted name is not required to strictly correspond with the family name, given name, etc.
    /// </remarks>
    string FormattedName { get; set; }

    /// <summary>
    /// Gets or sets the nickname of the vCard.
    /// </summary>
    string Nickname { get; set; }

    /// <summary>
    /// Gets or sets the birthdate of the vCard.
    /// </summary>
    IDateTime Birthdate { get; set; }

    /// <summary>
    /// Gets or sets the photo of the vCard.
    /// </summary>
    Photo Photo { get; set; }

    /// <summary>
    /// Gets or sets the anniversary of the vCard.
    /// </summary>
    IDateTime Anniversary { get; set; }

    /// <summary>
    /// Gets or sets the gender of the vCard.
    /// </summary>
    Gender Gender { get; set; }

    #endregion

    #region Delivery Addressing Properties

    /// <summary>
    /// Gets or sets a collection of <see cref="Address" /> objects for the vCard.
    /// </summary>
    IList<Address> Addresses { get; set; }

    #endregion

    #region Communications Properties

    /// <summary>
    /// Gets or sets a collection of <see cref="PhoneNumber" /> objects for the vCard.
    /// </summary>
    IList<PhoneNumber> PhoneNumbers { get; set; }

    /// <summary>
    /// Gets or sets a collection of <see cref="EmailAddress" /> objects for the vCard.
    /// </summary>
    IList<EmailAddress> EmailAddresses { get; set; }

    /// <summary>
    /// Gets or sets a collection of <see cref="IMPP" /> objects for the vCard.
    /// </summary>
    IList<IMPP> IMPPs { get; set; }

    /// <summary>
    /// Gets or sets a collection of <see cref="Url" /> objects for the vCard.
    /// </summary>
    IList<Url> Urls { get; set; }

    /// <summary>
    /// Gets or sets a collection of <see cref="Language" /> objects for the vCard.
    /// </summary>
    IList<Language> Languages { get; set; }

    #endregion

    #region Geographical Properties

    /// <summary>
    /// Gets or sets the time zone of the vCard.
    /// </summary>
    string TimeZone { get; set; }

    /// <summary>
    /// Gets or sets the geographic position of the vCard.
    /// </summary>
    GeographicPosition GeographicPosition { get; set; }

    #endregion

    #region Organizational Properties

    /// <summary>
    /// Gets or sets the job title of the vCard.
    /// </summary>
    string Title { get; set; }

    /// <summary>
    /// Gets or sets the role of the vCard.
    /// </summary>
    string Role { get; set; }

    /// <summary>
    /// Gets or sets the logo of the vCard.
    /// </summary>
    Logo Logo { get; set; }

    /// <summary>
    /// Gets or sets the organization of the vCard.
    /// </summary>
    Organization Organization { get; set; }

    /// <summary>
    /// Gets or sets a collection of <see cref="string" /> objects for the vCard.
    /// </summary>
    IList<string> Members { get; set; }

    /// <summary>
    /// Gets or sets a collection of <see cref="Related" /> objects for the vCard.
    /// </summary>
    IList<Related> RelatedObjects { get; set; }

    #endregion

    #region Explanatory Properties

    /// <summary>
    /// Gets or sets a collection of <see cref="DataTypes.Categories" /> objects for the vCard.
    /// </summary>
    Categories Categories { get; set; }

    /// <summary>
    /// Gets or sets a collection of <see cref="string" /> objects for the vCard.
    /// </summary>
    IList<string> Notes { get; set; }

    /// <summary>
    /// Gets or sets the name of the product that generated the vCard.
    /// </summary>
    string ProductId { get; set; }

    /// <summary>
    /// Gets or sets the revision date of the vCard.
    /// </summary>
    /// <remarks>
    /// The revision date is not automatically updated by the vCard when modifying properties.
    /// It is up to the developer to change the revision date as needed.
    /// </remarks>
    IDateTime RevisionDate { get; set; }

    /// <summary>
    /// Gets or sets the sound of the vCard.
    /// </summary>
    Sound Sound { get; set; }

    #endregion

    #region Security Properties

    /// <summary>
    /// Gets or sets the key of the vCard.
    /// </summary>
    Key Key { get; set; }

    #endregion

    #region Calendar Properties

    #endregion
}