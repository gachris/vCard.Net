using System.Text;
using vCard.Net.DataTypes;
using vCard.Net.Serialization;
using Xunit;

namespace vCard.Net.Tests;

public class vCard_v30SerializerTests
{
    [Theory]
    [InlineDataEx("Data/vCard_v30.vcf")]
    public void SerializeToString(string vCardData)
    {
        var serializer = new ComponentSerializer();
        var vCard = CreateCard();
        var vCardAsString = serializer.SerializeToString(vCard).Trim();

        // Assert
        Assert.Equal(vCardData.Trim(), vCardAsString);
    }

    [Theory]
    [InlineDataEx("Data/vCard_v30.vcf")]
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
            Version = vCardVersion.vCard3_0,
            Uid = "12345678-9abc-def0-1234-56789abcdef0",
            Anniversary = new vCardDateTime(2010, 06, 15)
            {
                HasTime = false
            },
            Key = new Key
            {
                KeyType = "X509",
                Encoding = "BASE64",
                Value = "/9j/4AAQSkZJRgABAQEAAAAAAAD..."
            },
            N = new StructuredName { FamilyName = "Doe", GivenName = "John", NamePrefix = "Mr", NameSuffix = "PhD" },
            FormattedName = "John Doe",
            Nickname = "Johnny",
            Photo = new Photo()
            {
                Encoding = "BASE64",
                Type = "JPEG",
                Value = "/9j/4AAQSkZJRgABAQEAAAAAAAD..."
            },
            Birthdate = new vCardDateTime(1980, 1, 1)
            {
                HasTime = false
            },
            Addresses =
            [
                new Address
                {
                    Types = ["WORK"],
                    StreetAddress = "1234 Company St",
                    Locality = "City",
                    Region = "State",
                    PostalCode = "12345",
                    Country = "USA"
                }
            ],
            Labels =
            [
                new Label
                {
                    Value = "1234 Company St\nCity, State 12345\nUSA",
                    Types = ["WORK"]
                }
            ],
            Telephones =
            [
                new Telephone
                {
                    Value = "+1-234-567-8900",
                    Types = ["WORK", "VOICE"]
                },
                new Telephone
                {
                    Value = "+1-234-567-8901",
                    Types = ["HOME", "VOICE"]
                }
            ],
            Emails =
            [
                new Email
                {
                    PreferredOrder = 1,
                    Value = "johndoe@example.com",
                    Types = ["INTERNET"]
                }
            ],
            Organization = new Organization
            {
                Name = "Company Inc.",
                UnitsString = "Software Division"
            },
            Title = "Software Engineer",
            Role = "Lead Developer",
            Logo = new Logo
            {
                Encoding = "BASE64",
                Type = "JPEG",
                Value = "/9j/4AAQSkZJRgABAQEAAAAAAAD..."
            },
            Note = "This is a detailed example of vCard 3.0.",
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
            Mailer = "Mozilla Thunderbird",
            Categories = new Categories
            {
                CategoriesString = "Friends,Colleagues"
            },
            GeographicPosition = new GeographicPosition()
            {
                Latitude = 37.7749,
                Longitude = -122.4194,
                IncludeGeoUriPrefix = false
            },
            Source = new Source()
            {
                Value = new Uri("http://example.com/johndoe.vcf")
            },
            SortString = "Doe",
            TimeZone = "+05:00",
            Agent = new CardComponents.vCard()
            {
                Version = vCardVersion.vCard3_0,
                Uid =   null,
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