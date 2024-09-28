using System.Text;
using vCard.Net.DataTypes;
using vCard.Net.Serialization;
using Xunit;

namespace vCard.Net.Tests;

public class vCard_v21SerializerTests
{
    [Theory]
    [InlineDataEx("Data/vCard_v21.vcf")]
    public void SerializeToString(string vCardData)
    {
        var serializer = new ComponentSerializer();
        var vCard = CreateCard();
        var vCardAsString = serializer.SerializeToString(vCard).Trim();

        // Assert
        Assert.Equal(vCardData.Trim(), vCardAsString);
    }

    [Theory]
    [InlineDataEx("Data/vCard_v21.vcf")]
    public void Deserialize(string vCardData)
    {
        var vCard = CreateCard();
        var bytes = Encoding.UTF8.GetBytes(vCardData);

        using var stream = new MemoryStream(bytes);
        using var streamReader = new StreamReader(stream, Encoding.UTF8);

        var vCardFromData = (CardComponents.vCard)SimpleDeserializer.Default.Deserialize(streamReader).First();

        // Assert
        Assert.True(vCard.Equals(vCardFromData));
    }

    private static CardComponents.vCard CreateCard()
    {
        var vCard = new CardComponents.vCard
        {
            Version = vCardVersion.vCard2_1,
            N = new StructuredName { FamilyName = "Doe", GivenName = "John", NamePrefix = "Mr", NameSuffix = "PhD" },
            FormattedName = "John Doe",
            Nickname = "Johnny",
            Photo = new Photo()
            {
                Encoding = "BASE64",
                ValueType = "JPEG",
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
                    Value = "+1 234 567 8900",
                    Types = ["WORK", "VOICE"]
                },
                new Telephone
                {
                    Value = "+1 234 567 8901",
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
                ValueType = "JPEG",
                Value = "/9j/4AAQSkZJRgABAQEAAAAAAAD..."
            },
            Note = "This is a detailed example of vCard 2.1.",
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
            Agent = new CardComponents.vCard()
            {
                Version = vCardVersion.vCard2_1,
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