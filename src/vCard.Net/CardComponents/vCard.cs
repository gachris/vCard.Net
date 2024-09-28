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
    /// <summary>
    /// Gets or sets the kind of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 3.0, 4.0
    /// </remarks>
    public virtual Kind Kind
    {
        get => Properties.Get<Kind>("KIND");
        set => Properties.Set("KIND", value);
    }

    /// <summary>
    /// Gets or sets the structured name of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 2.1, 3.0, 4.0
    /// </remarks>
    public virtual StructuredName N
    {
        get => Properties.Get<StructuredName>("N");
        set => Properties.Set("N", value);
    }

    /// <summary>
    /// Gets or sets the formatted name of the vCard.
    /// </summary>
    /// <remarks>
    /// This property allows the name of the vCard to be written in a manner specific to his or her culture.
    /// The formatted name is not required to strictly correspond with the family name, given name, etc.
    /// <para>Supported in: vCard 2.1, 3.0, 4.0</para>
    /// </remarks>
    public virtual string FormattedName
    {
        get => Properties.Get<string>("FN");
        set => Properties.Set("FN", value);
    }

    /// <summary>
    /// Gets or sets the nickname of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 3.0, 4.0
    /// </remarks>
    public virtual string Nickname
    {
        get => Properties.Get<string>("NICKNAME");
        set => Properties.Set("NICKNAME", value);
    }

    /// <summary>
    /// Gets or sets the photo of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 2.1, 3.0, 4.0
    /// </remarks>
    public virtual Photo Photo
    {
        get => Properties.Get<Photo>("PHOTO");
        set => Properties.Set("PHOTO", value);
    }

    /// <summary>
    /// Gets or sets the birthdate of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 2.1, 3.0, 4.0
    /// </remarks>
    public virtual IDateTime Birthdate
    {
        get => Properties.Get<IDateTime>("BDAY");
        set => Properties.Set("BDAY", value);
    }

    /// <summary>
    /// Gets or sets the anniversary of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 3.0, 4.0
    /// </remarks>
    public virtual IDateTime Anniversary
    {
        get => Properties.Get<IDateTime>("ANNIVERSARY");
        set => Properties.Set("ANNIVERSARY", value);
    }

    /// <summary>
    /// Gets or sets the gender of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 4.0
    /// </remarks>
    public virtual Gender Gender
    {
        get => Properties.Get<Gender>("GENDER");
        set => Properties.Set("GENDER", value);
    }

    /// <summary>
    /// Gets or sets the birth place of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 4.0
    /// </remarks>
    public virtual BirthPlace BirthPlace
    {
        get => Properties.Get<BirthPlace>("BIRTHPLACE");
        set => Properties.Set("BIRTHPLACE", value);
    }

    /// <summary>
    /// Gets or sets the death place of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 4.0
    /// </remarks>
    public virtual DeathPlace DeathPlace
    {
        get => Properties.Get<DeathPlace>("DEATHPLACE");
        set => Properties.Set("DEATHPLACE", value);
    }

    /// <summary>
    /// Gets or sets the death date of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 4.0
    /// </remarks>
    public virtual IDateTime DeathDate
    {
        get => Properties.Get<IDateTime>("DEATHDATE");
        set => Properties.Set("DEATHDATE", value);
    }

    /// <summary>
    /// Gets or sets a collection of <see cref="Address" /> objects for the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 2.1, 3.0, 4.0
    /// </remarks>
    public virtual IList<Address> Addresses
    {
        get => Properties.GetMany<Address>("ADR");
        set => Properties.Set("ADR", value);
    }

    /// <summary>
    /// Gets or sets the label of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 3.0, 4.0
    /// </remarks>
    public virtual IList<Label> Labels
    {
        get => Properties.GetMany<Label>("LABEL");
        set => Properties.Set("LABEL", value);
    }

    /// <summary>
    /// Gets or sets a collection of <see cref="Telephone" /> objects for the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 2.1, 3.0, 4.0
    /// </remarks>
    public virtual IList<Telephone> Telephones
    {
        get => Properties.GetMany<Telephone>("TEL");
        set => Properties.Set("TEL", value);
    }

    /// <summary>
    /// Gets or sets a collection of <see cref="Email" /> objects for the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 2.1, 3.0, 4.0
    /// </remarks>
    public virtual IList<Email> Emails
    {
        get => Properties.GetMany<Email>("EMAIL");
        set => Properties.Set("EMAIL", value);
    }

    /// <summary>
    /// Gets or sets a collection of <see cref="IMPP" /> objects for the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 3.0, 4.0
    /// </remarks>
    public virtual IList<IMPP> InstantMessagingProtocols
    {
        get => Properties.GetMany<IMPP>("IMPP");
        set => Properties.Set("IMPP", value);
    }

    /// <summary>
    /// Gets or sets a collection of <see cref="Language" /> objects for the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 3.0, 4.0
    /// </remarks>
    public virtual IList<Language> Languages
    {
        get => Properties.GetMany<Language>("LANG");
        set => Properties.Set("LANG", value);
    }

    /// <summary>
    /// Gets or sets the time zone of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 3.0, 4.0
    /// </remarks>
    public virtual string TimeZone
    {
        get => Properties.Get<string>("TZ");
        set => Properties.Set("TZ", value);
    }

    /// <summary>
    /// Gets or sets the geographic position of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 3.0, 4.0
    /// </remarks>
    public virtual GeographicPosition GeographicPosition
    {
        get => Properties.Get<GeographicPosition>("GEO");
        set => Properties.Set("GEO", value);
    }

    /// <summary>
    /// Gets or sets the job title of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 2.1, 3.0, 4.0
    /// </remarks>
    public virtual string Title
    {
        get => Properties.Get<string>("TITLE");
        set => Properties.Set("TITLE", value);
    }

    /// <summary>
    /// Gets or sets the role of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 3.0, 4.0
    /// </remarks>
    public virtual string Role
    {
        get => Properties.Get<string>("ROLE");
        set => Properties.Set("ROLE", value);
    }

    /// <summary>
    /// Gets or sets the sort string of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 3.0, 4.0
    /// </remarks>
    public virtual string SortString
    {
        get => Properties.Get<string>("SORT-STRING");
        set => Properties.Set("SORT-STRING", value);
    }

    /// <summary>
    /// Gets or sets the logo of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 3.0, 4.0
    /// </remarks>
    public virtual Logo Logo
    {
        get => Properties.Get<Logo>("LOGO");
        set => Properties.Set("LOGO", value);
    }

    /// <summary>
    /// Gets or sets the organization of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 2.1, 3.0, 4.0
    /// </remarks>
    public virtual Organization Organization
    {
        get => Properties.Get<Organization>("ORG");
        set => Properties.Set("ORG", value);
    }

    /// <summary>
    /// Gets or sets the note of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 2.1, 3.0, 4.0
    /// </remarks>
    public virtual string Note
    {
        get => Properties.Get<string>("NOTE");
        set => Properties.Set("NOTE", value);
    }

    /// <summary>
    /// Gets or sets the source of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 3.0, 4.0
    /// </remarks>
    public virtual Source Source
    {
        get => Properties.Get<Source>("SOURCE");
        set => Properties.Set("SOURCE", value);
    }

    /// <summary>
    /// Gets or sets the revision date of the vCard.
    /// </summary>
    /// <remarks>
    /// The revision date is not automatically updated by the vCard when modifying properties.
    /// It is up to the developer to change the revision date as needed.
    /// <para>Supported in: vCard 2.1, 3.0, 4.0</para>
    /// </remarks> 
    public virtual IDateTime RevisionDate
    {
        get => Properties.Get<IDateTime>("REV");
        set => Properties.Set("REV", value);
    }

    /// <summary>
    /// Gets or sets a collection of <see cref="Url" /> objects for the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 2.1, 3.0, 4.0
    /// </remarks>
    public virtual IList<Url> Urls
    {
        get => Properties.GetMany<Url>("URL");
        set => Properties.Set("URL", value);
    }

    /// <summary>
    /// Gets or sets the key of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 4.0
    /// </remarks>
    public virtual Key Key
    {
        get => Properties.Get<Key>("KEY");
        set => Properties.Set("KEY", value);
    }

    /// <summary>
    /// Gets or sets the mailer of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 2.1, 3.0, 4.0
    /// </remarks>
    public virtual string Mailer
    {
        get => Properties.Get<string>("MAILER");
        set => Properties.Set("MAILER", value);
    }

    /// <summary>
    /// Gets or sets a collection of <see cref="DataTypes.Categories" /> objects for the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 3.0, 4.0
    /// </remarks>
    public virtual Categories Categories
    {
        get => Properties.Get<Categories>("CATEGORIES");
        set => Properties.Set("CATEGORIES", value);
    }

    /// <summary>
    /// Gets or sets the expertise of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 4.0
    /// </remarks>
    public virtual Expertise Expertise
    {
        get => Properties.Get<Expertise>("EXPERTISE");
        set => Properties.Set("EXPERTISE", value);
    }

    /// <summary>
    /// Gets or sets the hobby of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 4.0
    /// </remarks>
    public virtual Hobby Hobby
    {
        get => Properties.Get<Hobby>("HOBBY");
        set => Properties.Set("HOBBY", value);
    }

    /// <summary>
    /// Gets or sets the interest of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 4.0
    /// </remarks>
    public virtual Interest Interest
    {
        get => Properties.Get<Interest>("INTEREST");
        set => Properties.Set("INTEREST", value);
    }

    /// <summary>
    /// Gets or sets a collection of <see cref="Related" /> objects for the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 4.0
    /// </remarks>
    public virtual IList<Related> RelatedObjects
    {
        get => Properties.GetMany<Related>("RELATED");
        set => Properties.Set("RELATED", value);
    }

    /// <summary>
    /// Gets or sets a collection of <see cref="ClientPidMap" /> objects for the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 4.0
    /// </remarks>
    public virtual IList<ClientPidMap> ClientPidMaps
    {
        get => Properties.GetMany<ClientPidMap>("CLIENTPIDMAP");
        set => Properties.Set("CLIENTPIDMAP", value);
    }

    /// <summary>
    /// Gets or sets a collection of <see cref="string" /> objects for the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 4.0
    /// </remarks>
    public virtual IList<string> Members
    {
        get => Properties.GetMany<string>("MEMBER");
        set => Properties.Set("MEMBER", value);
    }

    /// <summary>
    /// Gets or sets the sound of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 4.0
    /// </remarks>
    public virtual Sound Sound
    {
        get => Properties.Get<Sound>("SOUND");
        set => Properties.Set("SOUND", value);
    }

    /// <summary>
    /// Gets or sets the name of the product that generated the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 2.1, 3.0, 4.0
    /// </remarks>
    public virtual string ProductId
    {
        get => Properties.Get<string>("PRODID");
        set => Properties.Set("PRODID", value);
    }

    /// <summary>
    /// Gets or sets the XML of the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 4.0
    /// </remarks>
    public virtual Xml Xml
    {
        get => Properties.Get<Xml>("XML");
        set => Properties.Set("XML", value);
    }

    /// <summary>
    /// Gets or sets a collection of <see cref="vCard" /> objects for the vCard.
    /// </summary>
    /// <remarks>
    /// Supported in: vCard 2.1, 3.0, 4.0
    /// </remarks>
    public virtual IList<vCard> Agents
    {
        get => Properties.GetMany<vCard>("AGENT");
        set => Properties.Set("AGENT", value);
    }

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
        return string.Equals(Uid, other.Uid, StringComparison.OrdinalIgnoreCase)
                 && Equals(Source, other.Source)
                 && Equals(Kind, other.Kind)
                 && Equals(N, other.N)
                 && string.Equals(FormattedName, other.FormattedName, StringComparison.OrdinalIgnoreCase)
                 && string.Equals(Nickname, other.Nickname, StringComparison.OrdinalIgnoreCase)
                 && Equals(Birthdate, other.Birthdate)
                 && Equals(Photo, other.Photo)
                 && Equals(Anniversary, other.Anniversary)
                 && Equals(Gender, other.Gender)
                 && Equals(BirthPlace, other.BirthPlace)
                 && Equals(DeathPlace, other.DeathPlace)
                 && Equals(DeathDate, other.DeathDate)
                 && CollectionHelpers.Equals(Addresses, other.Addresses)
                 && CollectionHelpers.Equals(Labels, other.Labels)
                 && CollectionHelpers.Equals(Telephones, other.Telephones)
                 && CollectionHelpers.Equals(Emails, other.Emails)
                 && CollectionHelpers.Equals(InstantMessagingProtocols, other.InstantMessagingProtocols)
                 && CollectionHelpers.Equals(Languages, other.Languages)
                 && string.Equals(TimeZone, other.TimeZone, StringComparison.OrdinalIgnoreCase)
                 && Equals(GeographicPosition, other.GeographicPosition)
                 && string.Equals(Title, other.Title, StringComparison.OrdinalIgnoreCase)
                 && string.Equals(Role, other.Role, StringComparison.OrdinalIgnoreCase)
                 && Equals(Logo, other.Logo)
                 && Equals(Organization, other.Organization)
                 && string.Equals(Note, other.Note, StringComparison.OrdinalIgnoreCase)
                 && CollectionHelpers.Equals(Members, other.Members)
                 && CollectionHelpers.Equals(RelatedObjects, other.RelatedObjects)
                 && Equals(Categories, other.Categories)
                 && string.Equals(ProductId, other.ProductId, StringComparison.OrdinalIgnoreCase)
                 && Equals(RevisionDate, other.RevisionDate)
                 && Equals(Sound, other.Sound)
                 && Equals(Key, other.Key)
                 && Equals(Xml, other.Xml)
                 && CollectionHelpers.Equals(Agents, other.Agents)
                 && string.Equals(Mailer, other.Mailer, StringComparison.OrdinalIgnoreCase)
                 && Equals(Expertise, other.Expertise)
                 && Equals(Hobby, other.Hobby)
                 && Equals(Interest, other.Interest)
                 && string.Equals(SortString, other.SortString, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj is not null && (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((vCard)obj));
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Uid != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Uid) : 0;
            hashCode = (hashCode * 397) ^ (Source != null ? Source.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Kind != null ? Kind.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (N != null ? N.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (FormattedName != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(FormattedName) : 0);
            hashCode = (hashCode * 397) ^ (Nickname != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Nickname) : 0);
            hashCode = (hashCode * 397) ^ (Birthdate != null ? Birthdate.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Photo != null ? Photo.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Anniversary != null ? Anniversary.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Gender != null ? Gender.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (BirthPlace != null ? BirthPlace.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (DeathPlace != null ? DeathPlace.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (DeathDate != null ? DeathDate.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ CollectionHelpers.GetHashCode(Addresses);
            hashCode = (hashCode * 397) ^ (Labels != null ? Labels.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ CollectionHelpers.GetHashCode(Telephones);
            hashCode = (hashCode * 397) ^ CollectionHelpers.GetHashCode(Emails);
            hashCode = (hashCode * 397) ^ CollectionHelpers.GetHashCode(InstantMessagingProtocols);
            hashCode = (hashCode * 397) ^ CollectionHelpers.GetHashCode(Languages);
            hashCode = (hashCode * 397) ^ (TimeZone != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(TimeZone) : 0);
            hashCode = (hashCode * 397) ^ (GeographicPosition != null ? GeographicPosition.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Title != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Title) : 0);
            hashCode = (hashCode * 397) ^ (Role != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Role) : 0);
            hashCode = (hashCode * 397) ^ (Logo != null ? Logo.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Organization != null ? Organization.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ CollectionHelpers.GetHashCode(Members);
            hashCode = (hashCode * 397) ^ CollectionHelpers.GetHashCode(RelatedObjects);
            hashCode = (hashCode * 397) ^ (Categories != null ? Categories.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Note != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Note) : 0);
            hashCode = (hashCode * 397) ^ (ProductId != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(ProductId) : 0);
            hashCode = (hashCode * 397) ^ (RevisionDate != null ? RevisionDate.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Sound != null ? Sound.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Key != null ? Key.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Xml != null ? Xml.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Agents != null ? CollectionHelpers.GetHashCode(Agents) : 0);
            hashCode = (hashCode * 397) ^ (Mailer != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Mailer) : 0);
            hashCode = (hashCode * 397) ^ (Expertise != null ? Expertise.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Hobby != null ? Hobby.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Interest != null ? Interest.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (SortString != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(SortString) : 0);

            return hashCode;
        }
    }
}