using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using vCard.Net.DataTypes;
using vCard.Net.Utility;

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
public class vCard : UniqueComponent, IvCard
{
    #region General Properties

    /// <summary>
    /// Gets or sets the source of the vCard.
    /// </summary>
    public virtual Source Source
    {
        get => Properties.Get<Source>("SOURCE");
        set => Properties.Set("SOURCE", value);
    }

    /// <summary>
    /// Gets or sets the kind of the vCard.
    /// </summary>
    public virtual Kind Kind
    {
        get => Properties.Get<Kind>("KIND");
        set => Properties.Set("KIND", value);
    }

    #endregion

    #region Identification Properties

    /// <summary>
    /// Gets or sets the name of the vCard.
    /// </summary>
    public virtual Name N
    {
        get => Properties.Get<Name>("N");
        set => Properties.Set("N", value);
    }

    /// <summary>
    /// Gets or sets the formatted name of the vCard.
    /// </summary>
    /// <remarks>
    /// This property allows the name of the vCard to be written in a manner specific to his or her culture.
    /// The formatted name is not required to strictly correspond with the family name, given name, etc.
    /// </remarks>
    public virtual string FormattedName
    {
        get => Properties.Get<string>("FN");
        set => Properties.Set("FN", value);
    }

    /// <summary>
    /// Gets or sets the nickname of the vCard.
    /// </summary>
    public virtual string Nickname
    {
        get => Properties.Get<string>("NICKNAME");
        set => Properties.Set("NICKNAME", value);
    }

    /// <summary>
    /// Gets or sets the birthdate of the vCard.
    /// </summary>
    public virtual IDateTime Birthdate
    {
        get => Properties.Get<IDateTime>("BDAY");
        set => Properties.Set("BDAY", value);
    }

    /// <summary>
    /// Gets or sets the photo of the vCard.
    /// </summary>
    public virtual Photo Photo
    {
        get => Properties.Get<Photo>("PHOTO");
        set => Properties.Set("PHOTO", value);
    }

    /// <summary>
    /// Gets or sets the anniversary of the vCard.
    /// </summary>
    public virtual IDateTime Anniversary
    {
        get => Properties.Get<IDateTime>("ANNIVERSARY");
        set => Properties.Set("ANNIVERSARY", value);
    }

    /// <summary>
    /// Gets or sets the gender of the vCard.
    /// </summary>
    public virtual Gender Gender
    {
        get => Properties.Get<Gender>("GENDER");
        set => Properties.Set("GENDER", value);
    }

    #endregion

    #region Delivery Addressing Properties

    /// <summary>
    /// Gets or sets a collection of <see cref="Address" /> objects for the vCard.
    /// </summary>
    public virtual IList<Address> Addresses
    {
        get => Properties.GetMany<Address>("ADR");
        set => Properties.Set("ADR", value);
    }

    #endregion

    #region Communications Properties

    /// <summary>
    /// Gets or sets a collection of <see cref="PhoneNumber" /> objects for the vCard.
    /// </summary>
    public virtual IList<PhoneNumber> PhoneNumbers
    {
        get => Properties.GetMany<PhoneNumber>("TEL");
        set => Properties.Set("TEL", value);
    }

    /// <summary>
    /// Gets or sets a collection of <see cref="EmailAddress" /> objects for the vCard.
    /// </summary>
    public virtual IList<EmailAddress> EmailAddresses
    {
        get => Properties.GetMany<EmailAddress>("EMAIL");
        set => Properties.Set("EMAIL", value);
    }

    /// <summary>
    /// Gets or sets a collection of <see cref="IMPP" /> objects for the vCard.
    /// </summary>
    public virtual IList<IMPP> IMPPs
    {
        get => Properties.GetMany<IMPP>("IMPP");
        set => Properties.Set("IMPP", value);
    }

    /// <summary>
    /// Gets or sets a collection of <see cref="Url" /> objects for the vCard.
    /// </summary>
    public virtual IList<Url> Urls
    {
        get => Properties.GetMany<Url>("URL");
        set => Properties.Set("URL", value);
    }

    /// <summary>
    /// Gets or sets a collection of <see cref="Language" /> objects for the vCard.
    /// </summary>
    public virtual IList<Language> Languages
    {
        get => Properties.GetMany<Language>("LANG");
        set => Properties.Set("LANG", value);
    }

    #endregion

    #region Geographical Properties

    /// <summary>
    /// Gets or sets the time zone of the vCard.
    /// </summary>
    public virtual string TimeZone
    {
        get => Properties.Get<string>("TZ");
        set => Properties.Set("TZ", value);
    }

    /// <summary>
    /// Gets or sets the geographic position of the vCard.
    /// </summary>
    public virtual GeographicPosition GeographicPosition
    {
        get => Properties.Get<GeographicPosition>("GEO");
        set => Properties.Set("GEO", value);
    }

    #endregion

    #region Organizational Properties

    /// <summary>
    /// Gets or sets the job title of the vCard.
    /// </summary>
    public virtual string Title
    {
        get => Properties.Get<string>("TITLE");
        set => Properties.Set("TITLE", value);
    }

    /// <summary>
    /// Gets or sets the role of the vCard.
    /// </summary>
    public virtual string Role
    {
        get => Properties.Get<string>("ROLE");
        set => Properties.Set("ROLE", value);
    }

    /// <summary>
    /// Gets or sets the logo of the vCard.
    /// </summary>
    public virtual Logo Logo
    {
        get => Properties.Get<Logo>("LOGO");
        set => Properties.Set("LOGO", value);
    }

    /// <summary>
    /// Gets or sets the organization of the vCard.
    /// </summary>
    public virtual Organization Organization
    {
        get => Properties.Get<Organization>("ORG");
        set => Properties.Set("ORG", value);
    }

    /// <summary>
    /// Gets or sets a collection of <see cref="string" /> objects for the vCard.
    /// </summary>
    public virtual IList<string> Members
    {
        get => Properties.GetMany<string>("MEMBER");
        set => Properties.Set("MEMBER", value);
    }

    /// <summary>
    /// Gets or sets a collection of <see cref="Related" /> objects for the vCard.
    /// </summary>
    public virtual IList<Related> RelatedCollection
    {
        get => Properties.GetMany<Related>("RELATED");
        set => Properties.Set("RELATED", value);
    }

    #endregion

    #region Explanatory Properties

    /// <summary>
    /// Gets or sets a collection of <see cref="DataTypes.Categories" /> objects for the vCard.
    /// </summary>
    public virtual Categories Categories
    {
        get => Properties.Get<Categories>("CATEGORIES");
        set => Properties.Set("CATEGORIES", value);
    }

    /// <summary>
    /// Gets or sets a collection of <see cref="string" /> objects for the vCard.
    /// </summary>
    public virtual IList<string> Notes
    {
        get => Properties.GetMany<string>("NOTE");
        set => Properties.Set("NOTE", value);
    }

    /// <summary>
    /// Gets or sets the name of the product that generated the vCard.
    /// </summary>
    public virtual string ProductId
    {
        get => Properties.Get<string>("PRODID");
        set => Properties.Set("PRODID", value);
    }

    /// <summary>
    /// Gets or sets the revision date of the vCard.
    /// </summary>
    /// <remarks>
    /// The revision date is not automatically updated by the vCard when modifying properties.
    /// It is up to the developer to change the revision date as needed.
    /// </remarks>
    public virtual IDateTime RevisionDate
    {
        get => Properties.Get<IDateTime>("REV");
        set => Properties.Set("REV", value);
    }

    /// <summary>
    /// Gets or sets the sound of the vCard.
    /// </summary>
    public virtual Sound Sound
    {
        get => Properties.Get<Sound>("SOUND");
        set => Properties.Set("SOUND", value);
    }

    #endregion

    #region Security Properties

    /// <summary>
    /// Gets or sets the key of the vCard.
    /// </summary>
    public virtual Key Key
    {
        get => Properties.Get<Key>("KEY");
        set => Properties.Set("KEY", value);
    }

    #endregion

    #region Calendar Properties

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="vCard"/> class.
    /// </summary>
    public vCard()
    {
        Name = Components.VCARD;
        Initialize();
        EnsureProperties();
    }

    /// <summary>
    /// Initializes the vCard object.
    /// </summary>
    private void Initialize()
    {
    }

    /// <summary>
    /// Ensures that all properties required for the vCard are properly initialized.
    /// </summary>
    private void EnsureProperties()
    {
    }

    /// <summary>
    /// Invoked during deserialization to initialize the vCard object.
    /// </summary>
    /// <param name="context">The streaming context.</param>
    protected override void OnDeserializing(StreamingContext context)
    {
        base.OnDeserializing(context);

        Initialize();
    }

    /// <summary>
    /// Determines whether the specified vCard object is equal to the current vCard object.
    /// </summary>
    /// <param name="other">The vCard to compare with the current vCard.</param>
    /// <returns><c>true</c> if the specified vCard is equal to the current vCard; otherwise, <c>false</c>.</returns>
    protected bool Equals(vCard other)
    {
        var result = string.Equals(Uid, other.Uid, StringComparison.OrdinalIgnoreCase)
        && Equals(Source, other.Source)
        && Equals(Kind, other.Kind)
        && Equals(N, other.N)
        && string.Equals(FormattedName, other.FormattedName, StringComparison.OrdinalIgnoreCase)
        && string.Equals(Nickname, other.Nickname, StringComparison.OrdinalIgnoreCase)
        && Equals(Birthdate, other.Birthdate)
        && Equals(Photo, other.Photo)
        && Equals(Anniversary, other.Anniversary)
        && Equals(Gender, other.Gender)
        && CollectionHelpers.Equals(Addresses, other.Addresses)
        && CollectionHelpers.Equals(PhoneNumbers, other.PhoneNumbers)
        && CollectionHelpers.Equals(EmailAddresses, other.EmailAddresses)
        && CollectionHelpers.Equals(IMPPs, other.IMPPs)
        && CollectionHelpers.Equals(Languages, other.Languages)
        && Equals(TimeZone, other.TimeZone)
        && Equals(GeographicPosition, other.GeographicPosition)
        && string.Equals(Title, other.Title, StringComparison.OrdinalIgnoreCase)
        && string.Equals(Role, other.Role, StringComparison.OrdinalIgnoreCase)
        && Equals(Logo, other.Logo)
        && Equals(Organization, other.Organization)
        && CollectionHelpers.Equals(Members, other.Members)
        && CollectionHelpers.Equals(RelatedCollection, other.RelatedCollection)
        && Equals(Categories, other.Categories)
        && CollectionHelpers.Equals(Notes, other.Notes)
        && string.Equals(ProductId, other.ProductId, StringComparison.OrdinalIgnoreCase)
        && Equals(RevisionDate, other.RevisionDate)
        && Equals(Sound, other.Sound)
        && Equals(Key, other.Key);
        return result;
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj is null ? false : ReferenceEquals(this, obj) ? true : obj.GetType() == GetType() && Equals((vCard)obj);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Uid.GetHashCode();
            hashCode = hashCode * 397 ^ (Source?.GetHashCode() ?? 0);
            hashCode = hashCode * 397 ^ (Kind?.GetHashCode() ?? 0);
            hashCode = hashCode * 397 ^ (N?.GetHashCode() ?? 0);
            hashCode = hashCode * 397 ^ (FormattedName?.GetHashCode() ?? 0);
            hashCode = hashCode * 397 ^ (Nickname?.GetHashCode() ?? 0);
            hashCode = hashCode * 397 ^ (Birthdate?.GetHashCode() ?? 0);
            hashCode = hashCode * 397 ^ (Photo?.GetHashCode() ?? 0);
            hashCode = hashCode * 397 ^ (Anniversary?.GetHashCode() ?? 0);
            hashCode = hashCode * 397 ^ (Gender?.GetHashCode() ?? 0);
            hashCode = hashCode * 397 ^ CollectionHelpers.GetHashCode(Addresses);
            hashCode = hashCode * 397 ^ CollectionHelpers.GetHashCode(PhoneNumbers);
            hashCode = hashCode * 397 ^ CollectionHelpers.GetHashCode(EmailAddresses);
            hashCode = hashCode * 397 ^ CollectionHelpers.GetHashCode(IMPPs);
            hashCode = hashCode * 397 ^ CollectionHelpers.GetHashCode(Languages);
            hashCode = hashCode * 397 ^ (TimeZone?.GetHashCode() ?? 0);
            hashCode = hashCode * 397 ^ (GeographicPosition?.GetHashCode() ?? 0);
            hashCode = hashCode * 397 ^ (Title?.GetHashCode() ?? 0);
            hashCode = hashCode * 397 ^ (Role?.GetHashCode() ?? 0);
            hashCode = hashCode * 397 ^ (Logo?.GetHashCode() ?? 0);
            hashCode = hashCode * 397 ^ (Organization?.GetHashCode() ?? 0);
            hashCode = hashCode * 397 ^ CollectionHelpers.GetHashCode(Members);
            hashCode = hashCode * 397 ^ CollectionHelpers.GetHashCode(RelatedCollection);
            hashCode = hashCode * 397 ^ (Categories?.GetHashCode() ?? 0);
            hashCode = hashCode * 397 ^ CollectionHelpers.GetHashCode(Notes);
            hashCode = hashCode * 397 ^ (ProductId?.GetHashCode() ?? 0);
            hashCode = hashCode * 397 ^ (RevisionDate?.GetHashCode() ?? 0);
            hashCode = hashCode * 397 ^ (Sound?.GetHashCode() ?? 0);
            hashCode = hashCode * 397 ^ (Key?.GetHashCode() ?? 0);
            return hashCode;
        }
    }
}