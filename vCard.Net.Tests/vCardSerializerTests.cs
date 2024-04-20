using System.Text;
using vCard.Net.DataTypes;
using vCard.Net.Serialization;

namespace vCard.Net.Tests;

public class vCardSerializerTests
{
    [Theory]
    [InlineDataEx("Data/vCard_1.vcf")]
    public void SerializeToString(string vCardData)
    {
        var serializer = new ComponentSerializer();
        var vCard = CreateCard();
        var vCardAsString = serializer.SerializeToString(vCard).Trim();

        // Assert
        Assert.Equal(vCardData.Trim(), vCardAsString);
    }

    [Theory]
    [InlineDataEx("Data/vCard_1.vcf")]
    public void Deserialize(string vCardData)
    {
        var vCard = CreateCard();
        var bytes = Encoding.UTF8.GetBytes(vCardData);
        using var stream = new MemoryStream(bytes);
        var vCardFromData = (CardComponents.vCard)SimpleDeserializer.Default.Deserialize(new StreamReader(stream, Encoding.UTF8)).First();

        // Assert
        Assert.True(vCard.Equals(vCardFromData));
    }

    private static CardComponents.vCard CreateCard()
    {
        var vCard = new CardComponents.vCard
        {
            Version = vCardVersion.vCard40,
            Uid = "b6d294b1-6187-4abb-b2ca-abe7842e6103",
            Source = new Source() { Value = new Uri("https://www.example.com/source") },
            Kind = new Kind { CardKind = KindType.Individual },
            N = new Name { FamilyName = "Smith", GivenName = "John" },
            FormattedName = "John Smith",
            Nickname = "Johnny",
            Birthdate = new vCardDateTime(1980, 1, 1) { HasTime = false, TzId = TimeZoneInfo.Utc.Id },
            Photo = new Photo() { Value = "https://www.example.com/photo.jpg", ValueType = "URI" },
            Anniversary = new vCardDateTime(2005, 5, 5) { HasTime = false, TzId = TimeZoneInfo.Utc.Id },
            Gender = new Gender() { Sex = 'M' },
            Addresses = new List<Address>
            {
                new Address
                {
                    Types = new List<string>(){ "work" },
                    StreetAddress = "123 Main Street",
                    Locality = "Anytown",
                    Region = "NY",
                    PostalCode = "12345",
                    Country = "USA"
                }
            },
            PhoneNumbers = new List<PhoneNumber>
            {
                new PhoneNumber
                {
                    Value = "+123456789",
                    Types = new List<string>(){ "work", "voice" }
                },
                new PhoneNumber
                {
                    Value = "+123456790",
                    Types = new List<string>(){ "cell" }
                }
            },
            EmailAddresses = new List<EmailAddress>
            {
                new EmailAddress
                {
                    Value = "john.smith@example.com",
                    Types = new List<string>(){ "work" }
                }
            },
            IMPPs = new List<IMPP>
            {
                new IMPP
                {
                    Types =new List<string> { "home" },
                    Value = "skype:john.smith"
                }
            },
            Urls = new List<Url>
            {
                new Url { Value = "https://www.example.com/john_smith" }
            },
            Languages = new List<Language>
            {
                new Language { Value = "en" }
            },
            TimeZone = "America/New_York",
            GeographicPosition = new GeographicPosition
            {
                Latitude = 37.386013,
                Longitude = -122.082932
            },
            Title = "CEO",
            Role = "Manager",
            Logo = new Logo { Value = "https://www.example.com/logo.png", ValueType = "URI" },
            Organization = new Organization { Name = "Example Company", UnitsString = "Marketing Department" },
            Members = new List<string> { "john.smith@example.com" },
            RelatedCollection = new List<Related>
            {
                new Related
                {
                    Types = new List<string>(){ "friend" },
                    Value = "john.doe@example.com"
                }
            },
            Categories = new Categories { CategoriesString = "Family, Friends" },
            Notes = new List<string> { "This is a note about John Smith." },
            ProductId = "MyVCardGenerator",
            RevisionDate = new vCardDateTime(2024, 4, 20, 12, 0, 0) { TzId = TimeZoneInfo.Utc.Id },
            Sound = new Sound { Value = "https://www.example.com/sound.mp3", ValueType = "URI" },
            Key = new Key { KeyType = "PGP", Value = "http://example.com/key.pgp" }
        };

        return vCard;
    }
}