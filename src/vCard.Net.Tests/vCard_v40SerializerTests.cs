using System.Text;
using vCard.Net.DataTypes;
using vCard.Net.Serialization;
using Xunit;

namespace vCard.Net.Tests;

public class vCard_v40SerializerTests
{
    [Theory]
    [InlineDataEx("Data/vCard_v40.vcf")]
    public void SerializeToString(string vCardData)
    {
        var serializer = new ComponentSerializer();
        var vCard = CreateCard();
        var vCardAsString = serializer.SerializeToString(vCard).Trim();

        // Assert
        Assert.Equal(vCardData.Trim(), vCardAsString);
    }

    [Theory]
    [InlineDataEx("Data/vCard_v40.vcf")]
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
            Version = vCardVersion.vCard4_0,
            Uid = "12345678-9abc-def0-1234-56789abcdef0",
            Anniversary = new vCardDateTime(2010, 06, 15)
            {
                HasTime = false
            },
            DeathDate = new vCardDateTime(2070, 05, 31)
            {
                HasTime = false
            },
            Languages =
            [
                new Language()
                {
                    PreferredOrder = 1,
                    Value = "en"
                }
            ],
            Hobby = "Hiking, Photography",
            Interest = "Art, Technology",
            Kind = new Kind()
            {
                CardKind = KindType.Individual
            },
            DeathPlace = "City, Country",
            Expertise = "Python Programming, AI Development",
            Gender = new Gender
            {
                Sex = 'M'
            },
            Key = new Key
            {
                KeyType = "pgp",
                Value = "http://www.johndoe.com/pgpkey.asc"
            },
            N = new StructuredName { FamilyName = "Doe", GivenName = "John", NamePrefix = "Mr", NameSuffix = "PhD" },
            FormattedName = "John Doe",
            Nickname = "Johnny",
            Photo = new Photo()
            {
                MediaType = "image/jpeg",
                Value = "http://www.johndoe.com/photo.jpg"
            },
            Birthdate = new vCardDateTime(1980, 1, 1)
            {
                HasTime = false
            },
            Addresses =
            [
                new Address
                {
                    Types = ["work"],
                    StreetAddress = "1234 Company St",
                    Locality = "City",
                    Region = "State",
                    PostalCode = "12345",
                    Country = "USA",
                    //new Label
                    //{
                    //    Value = "1234 Company St\nCity, State 12345\nUSA",
                    //    Types = ["WORK"]
                    //}
                }
            ],
            Telephones =
            [
                new Telephone
                {
                    Value = "+1-234-567-8900",
                    Types = ["work", "voice"]
                },
                new Telephone
                {
                    Value = "+1-234-567-8901",
                    Types = ["home", "voice"]
                }
            ],
            Emails =
            [
                new Email
                {
                    Value = "johndoe@example.com"
                }
            ],
            BirthPlace = "City, Country",
            Organization = new Organization
            {
                Name = "Company Inc.",
                UnitsString = "Software Division"
            },
            InstantMessagingProtocols =
            [
                new IMPP()
                {
                    Value = "skype:johndoe",
                    ServiceType = "Skype"
                }
            ],
            Title = "Software Engineer",
            Role = "Lead Developer",
            Logo = new Logo
            {
                MediaType = "image/jpeg",
                Value = "http://www.johndoe.com/logo.jpg"
            },
            ProductId = "-//John Doe Corporation//vCard 4.0//EN",
            RelatedObjects =
            [
                new Related()
                {
                    Types = ["sibling"],
                    Value = "urn:uuid:98765432-1abc-def0-1234-56789abcdef0"
                }
            ],
            Members =
            [
                "urn:uuid:12345678-9abc-def0-1234-56789abcdef1"
            ],
            Note = "This is a detailed example of vCard 4.0 with extended properties.",
            Urls =
            [
                new Url
                {
                    Value = "http://www.johndoe.com"
                }
            ],
            RevisionDate = new vCardDateTime(2023, 09, 01, 12, 0, 0)
            {
                TzId = TimeZoneInfo.Utc.Id
            },
            Categories = new Categories
            {
                CategoriesString = "Friends,Colleagues"
            },
            GeographicPosition = new GeographicPosition()
            {
                Latitude = 37.7749,
                Longitude = -122.4194,
                IncludeGeoUriPrefix = true
            },
            Source = new Source()
            {
                Value = new Uri("http://example.com/johndoe.vcf")
            },
            SortString = "Doe",
            TimeZone = "America/New_York",
            Sound = new Sound
            {
                MediaType = "audio/wav",
                Value = "http://www.johndoe.com/name.wav"
            },
            ClientPidMaps =
            [
                new ClientPidMap()
                {
                    Id = 2,
                    Value = "urn:uuid:12345678-9abc-def0-1234-56789abcdef0"
                }
            ],
            Xml = new Xml()
            {
                Value = "<extended-info><social-profile>http://twitter.com/johndoe</social-profile></extended-info>"
            },
            Agent = new CardComponents.vCard()
            {
                Version = vCardVersion.vCard4_0,
                Uid = null,
                FormattedName = "Jane Doe",
                Telephones =
                [
                    new Telephone(){
                        Value = "+1 987 654 3210",
                    }
                ]
            }
        };

        return vCard;
    }
}