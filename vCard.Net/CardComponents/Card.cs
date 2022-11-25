using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using vCard.Net.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.CardComponents
{
    /// <inheritdoc/>  
    public class Card : UniqueComponent, ICard
    {
        #region General Properties

        /// <summary>
        /// The source of the the vCard.
        /// </summary>
        public virtual Source Source
        {
            get => Properties.Get<Source>("SOURCE");
            set => Properties.Set("SOURCE", value);
        }

        /// <summary>
        /// The kind of the the vCard.
        /// </summary>
        public virtual Kind Kind
        {
            get => Properties.Get<Kind>("KIND");
            set => Properties.Set("KIND", value);
        }

        #endregion

        #region Identification Properties

        /// <summary>
        /// The name of the vCard.
        /// </summary>
        public virtual Name N
        {
            get => Properties.Get<Name>("N");
            set => Properties.Set("N", value);
        }

        /// <summary>
        /// The formatted name of the vCard.
        /// </summary>
        /// <remarks>
        ///     This property allows the name of the vCard to be
        ///     written in a manner specific to his or her culture.
        ///     The formatted name is not required to strictly
        ///     correspond with the family name, given name, etc.
        /// </remarks>
        public virtual string FormattedName
        {
            get => Properties.Get<string>("FN");
            set => Properties.Set("FN", value);
        }

        /// <summary>
        /// The nickname of the vCard.
        /// </summary>
        public virtual string Nickname
        {
            get => Properties.Get<string>("NICKNAME");
            set => Properties.Set("NICKNAME", value);
        }

        /// <summary>
        /// The birthdate of the vCard.
        /// </summary>
        public virtual IDateTime Birthdate
        {
            get => Properties.Get<IDateTime>("BDAY");
            set => Properties.Set("BDAY", value);
        }

        ///// <summary>
        ///// The photo of the vCard.
        ///// </summary>
        //public virtual Photo Photo
        //{
        //    get => Properties.Get<Photo>("PHOTO");
        //    set => Properties.Set("PHOTO", value);
        //}

        /// <summary>
        /// The anniversary of the vCard.
        /// </summary>
        public virtual IDateTime Anniversary
        {
            get => Properties.Get<IDateTime>("ANNIVERSARY");
            set => Properties.Set("ANNIVERSARY", value);
        }

        /// <summary>
        /// The gender of the vCard.
        /// </summary>
        public virtual Gender Gender
        {
            get => Properties.Get<Gender>("GENDER");
            set => Properties.Set("GENDER", value);
        }

        #endregion

        #region Delivery Addressing Properties

        ///// <summary>
        ///// A collection of <see cref="Address" /> objects for the vCard.
        ///// </summary>
        //public virtual IList<Address> AddressCollection
        //{
        //    get => Properties.GetMany<Address>("ADR");
        //    set => Properties.Set("ADR", value);
        //}

        #endregion

        #region Communications Properties

        ///// <summary>
        ///// A collection of <see cref="Telephone" /> objects for the vCard.
        ///// </summary>
        //public virtual IList<Telephone> TelephoneCollection
        //{
        //    get => Properties.GetMany<Telephone>("TEL");
        //    set => Properties.Set("TEL", value);
        //}

        ///// <summary>
        ///// A collection of <see cref="Email" /> objects for the vCard.
        ///// </summary>
        //public virtual IList<Email> EmailCollection
        //{
        //    get => Properties.GetMany<Email>("EMAIL");
        //    set => Properties.Set("EMAIL", value);
        //}

        ///// <summary>
        ///// A collection of <see cref="IMPP" /> objects for the vCard.
        ///// </summary>
        //public virtual IList<IMPP> IMPPCollection
        //{
        //    get => Properties.GetMany<IMPP>("IMPP");
        //    set => Properties.Set("IMPP", value);
        //}

        ///// <summary>
        ///// A collection of <see cref="Language" /> objects for the vCard.
        ///// </summary>
        //public virtual IList<Language> LanguageCollection
        //{
        //    get => Properties.GetMany<Language>("LANG");
        //    set => Properties.Set("LANG", value);
        //}

        #endregion

        #region Geographical Properties

        ///// <summary>
        ///// The time zone of the vCard.
        ///// </summary>
        //public virtual DataTypes.TimeZone TimeZone
        //{
        //    get => Properties.Get<DataTypes.TimeZone>("TZ");
        //    set => Properties.Set("TZ", value);
        //}

        /// <summary>
        /// The geographic position of the vCard.
        /// </summary>
        public virtual GeographicPosition GeographicPosition
        {
            get => Properties.Get<GeographicPosition>("GEO");
            set => Properties.Set("GEO", value);
        }

        #endregion

        #region Organizational Properties

        /// <summary>
        /// The job title of the vCard.
        /// </summary>
        public virtual string Title
        {
            get => Properties.Get<string>("TITLE");
            set => Properties.Set("TITLE", value);
        }

        /// <summary>
        /// The role of the vCard.
        /// </summary>
        public virtual string Role
        {
            get => Properties.Get<string>("ROLE");
            set => Properties.Set("ROLE", value);
        }

        /// <summary>
        /// The logo of the vCard.
        /// </summary>
        //public virtual Logo Logo
        //{
        //    get => Properties.Get<Logo>("LOGO");
        //    set => Properties.Set("LOGO", value);
        //}

        /// <summary>
        /// The organization of the vCard.
        /// </summary>
        public virtual Organization Organization
        {
            get => Properties.Get<Organization>("ORG");
            set => Properties.Set("ORG", value);
        }

        /// <summary>
        /// A collection of <see cref="string" /> objects for the vCard.
        /// </summary>
        public virtual IList<string> MemberCollection
        {
            get => Properties.GetMany<string>("MEMBER");
            set => Properties.Set("MEMBER", value);
        }

        ///// <summary>
        ///// A collection of <see cref="Related" /> objects for the vCard.
        ///// </summary>
        //public virtual IList<Related> RelatedCollection
        //{
        //    get => Properties.GetMany<Related>("RELATED");
        //    set => Properties.Set("RELATED", value);
        //}

        #endregion

        #region Explanatory Properties

        /// <summary>
        /// A collection of <see cref="DataTypes.Categories" /> objects for the vCard.
        /// </summary>
        public virtual Categories Categories
        {
            get => Properties.Get<Categories>("CATEGORIES");
            set => Properties.Set("CATEGORIES", value);
        }

        /// <summary>
        /// A collection of <see cref="string" /> objects for the vCard.
        /// </summary>
        public virtual IList<string> NoteCollection
        {
            get => Properties.GetMany<string>("NOTE");
            set => Properties.Set("NOTE", value);
        }

        /// <summary>
        /// The name of the product that generated the vCard.
        /// </summary>
        public virtual string ProductId
        {
            get => Properties.Get<string>("PRODID");
            set => Properties.Set("PRODID", value);
        }

        /// <summary>
        /// The revision date of the vCard.
        /// </summary>
        /// <remarks>
        ///     The revision date is not automatically updated by the
        ///     vCard when modifying properties. It is up to the
        ///     developer to change the revision date as needed.
        /// </remarks>
        public virtual IDateTime RevisionDate
        {
            get => Properties.Get<IDateTime>("REV");
            set => Properties.Set("REV", value);
        }

        ///// <summary>
        ///// The sound of the vCard.
        ///// </summary>
        //public virtual Sound Sound
        //{
        //    get => Properties.Get<Sound>("URL");
        //    set => Properties.Set("URL", value);
        //}

        /// <summary>
        /// A collection of <see cref="ClientPidMap" /> objects for the vCard.
        /// </summary>
        public virtual IList<ClientPidMap> ClientPidMapCollection
        {
            get => Properties.GetMany<ClientPidMap>("CLIENTPIDMAP");
            set => Properties.Set("CLIENTPIDMAP", value);
        }

        /// <summary>
        /// A collection of <see cref="Uri" /> objects for the vCard.
        /// </summary>
        public virtual IList<Uri> UrlCollection
        {
            get => Properties.GetMany<Uri>("URL");
            set => Properties.Set("URL", value);
        }

        #endregion

        #region Security Properties

        /// <summary>
        /// The key of the vCard.
        /// </summary>
        public virtual Key Key
        {
            get => Properties.Get<Key>("KEY");
            set => Properties.Set("KEY", value);
        }

        #endregion

        #region Calendar Properties

        #endregion

        public Card()
        {
            Name = Components.VCARD;
            Initialize();
            EnsureProperties();
        }

        private void Initialize()
        {
        }

        private void EnsureProperties()
        {
        }

        protected override void OnDeserializing(StreamingContext context)
        {
            base.OnDeserializing(context);

            Initialize();
        }

        protected bool Equals(Card other)
        {
            var result = string.Equals(Uid, other.Uid, StringComparison.OrdinalIgnoreCase)
            && Equals(Source, other.Source)
            && Equals(Kind, other.Kind)
            && Equals(N, other.N)
            && string.Equals(FormattedName, other.FormattedName, StringComparison.OrdinalIgnoreCase)
            && string.Equals(Nickname, other.Nickname, StringComparison.OrdinalIgnoreCase)
            && Equals(Birthdate, other.Birthdate)
            //&& Equals(Photo, other.Photo)
            && Equals(Anniversary, other.Anniversary)
            && Equals(Gender, other.Gender)
            //&& CollectionHelpers.Equals(AddressCollection, other.AddressCollection)
            //&& CollectionHelpers.Equals(TelephoneCollection, other.TelephoneCollection)
            //&& CollectionHelpers.Equals(EmailCollection, other.EmailCollection)
            //&& CollectionHelpers.Equals(IMPPCollection, other.IMPPCollection)
            //&& CollectionHelpers.Equals(LanguageCollection, other.LanguageCollection)
            //&& Equals(TimeZone, other.TimeZone)
            && Equals(GeographicPosition, other.GeographicPosition)
            && string.Equals(Title, other.Title, StringComparison.OrdinalIgnoreCase)
            && string.Equals(Role, other.Role, StringComparison.OrdinalIgnoreCase)
            //&& Equals(Logo, other.Logo)
            && Equals(Organization, other.Organization)
            && CollectionHelpers.Equals(MemberCollection, other.MemberCollection)
            //&& CollectionHelpers.Equals(RelatedCollection, other.RelatedCollection)
            && Equals(Categories, other.Categories)
            && CollectionHelpers.Equals(NoteCollection, other.NoteCollection)
            && string.Equals(ProductId, other.ProductId, StringComparison.OrdinalIgnoreCase)
            && Equals(RevisionDate, other.RevisionDate)
            //&& Equals(Sound, other.Sound)
            && CollectionHelpers.Equals(ClientPidMapCollection, other.ClientPidMapCollection)
            && CollectionHelpers.Equals(UrlCollection, other.UrlCollection)
            && Equals(Key, other.Key);
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Card)obj);
        }

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
                //hashCode = hashCode * 397 ^ (Photo?.GetHashCode() ?? 0);
                hashCode = hashCode * 397 ^ (Anniversary?.GetHashCode() ?? 0);
                hashCode = hashCode * 397 ^ (Gender?.GetHashCode() ?? 0);
                //hashCode = hashCode * 397 ^ CollectionHelpers.GetHashCode(AddressCollection);
                //hashCode = hashCode * 397 ^ CollectionHelpers.GetHashCode(TelephoneCollection);
                //hashCode = hashCode * 397 ^ CollectionHelpers.GetHashCode(EmailCollection);
                //hashCode = hashCode * 397 ^ CollectionHelpers.GetHashCode(IMPPCollection);
                //hashCode = hashCode * 397 ^ CollectionHelpers.GetHashCode(LanguageCollection);
                //hashCode = hashCode * 397 ^ (TimeZone?.GetHashCode() ?? 0);
                hashCode = hashCode * 397 ^ (GeographicPosition?.GetHashCode() ?? 0);
                hashCode = hashCode * 397 ^ (Title?.GetHashCode() ?? 0);
                hashCode = hashCode * 397 ^ (Role?.GetHashCode() ?? 0);
                //hashCode = hashCode * 397 ^ (Logo?.GetHashCode() ?? 0);
                hashCode = hashCode * 397 ^ (Organization?.GetHashCode() ?? 0);
                hashCode = hashCode * 397 ^ CollectionHelpers.GetHashCode(MemberCollection);
                //hashCode = hashCode * 397 ^ CollectionHelpers.GetHashCode(RelatedCollection);
                hashCode = hashCode * 397 ^ (Categories?.GetHashCode() ?? 0);
                hashCode = hashCode * 397 ^ CollectionHelpers.GetHashCode(NoteCollection);
                hashCode = hashCode * 397 ^ (ProductId?.GetHashCode() ?? 0);
                hashCode = hashCode * 397 ^ (RevisionDate?.GetHashCode() ?? 0);
                //hashCode = hashCode * 397 ^ (Sound?.GetHashCode() ?? 0);
                hashCode = hashCode * 397 ^ CollectionHelpers.GetHashCode(ClientPidMapCollection);
                hashCode = hashCode * 397 ^ CollectionHelpers.GetHashCode(UrlCollection);
                hashCode = hashCode * 397 ^ (Key?.GetHashCode() ?? 0);
                return hashCode;
            }
        }
    }
}