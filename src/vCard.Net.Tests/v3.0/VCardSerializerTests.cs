using System.Text;
using vCard.Net.CardComponents;
using vCard.Net.DataTypes;
using vCard.Net.Serialization;
using Xunit;

namespace vCard.Net.Tests.v3_0;

public class VCardSerializerTests
{
    #region Fields/Consts

    private readonly string _dataFilePath;

    #endregion

    public VCardSerializerTests()
    {
        // Use AppContext.BaseDirectory to get the test directory
        _dataFilePath = Path.Combine(AppContext.BaseDirectory, "v3.0/Data/John Doe.vcf");
    }

    [Fact]
    public void SerializeToString()
    {
        // Ensure the file exists
        Assert.True(File.Exists(_dataFilePath), $"File not found: {_dataFilePath}");

        // Read the content of the file (you named it jsonData, but it's probably vCard data)
        var vCardData = File.ReadAllText(_dataFilePath);

        // Create a vCard object (assuming you have the CreateCard method implemented)
        var vCard = CreateCard();

        // Serialize vCard to string
        var serializer = new ComponentSerializer();
        var vCardAsString = serializer.SerializeToString(vCard);

        // Assert that the serialized string matches the data from the file
        Assert.Equal(vCardData, vCardAsString);
    }

    [Fact]
    public void Deserialize()
    {
        // Ensure the file exists
        Assert.True(File.Exists(_dataFilePath), $"File not found: {_dataFilePath}");

        // Read the content of the file (you named it jsonData, but it's probably vCard data)
        var vCardData = File.ReadAllText(_dataFilePath);

        // Create a vCard object
        var vCard = CreateCard();

        // Deserialize the vCardData string
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(vCardData));
        using var streamReader = new StreamReader(stream, Encoding.UTF8);

        // Use the deserializer to convert the string back into a vCard object
        var vCardFromData = (VCard)SimpleDeserializer.Default.Deserialize(streamReader).First();

        // Assert
        var areEquals = vCard.Equals(vCardFromData);
        Assert.True(areEquals);
    }

    private static CardComponents.VCard CreateCard()
    {
        var vCard = new CardComponents.VCard
        {
            Version = VCardVersion.vCard3_0,
            Uid = "12345678-9abc-def0-1234-56789abcdef0",
            Anniversary = new VCardDateTime(2010, 06, 15)
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
            Birthdate = new VCardDateTime(1980, 1, 1)
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
                    Value = "johndoe@example.com",
                    Types = ["PREF", "INTERNET"]
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
            RevisionDate = new VCardDateTime(2023, 09, 01, 12, 0, 0)
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
            Agents =
            [
                new CardComponents.VCard()
                {
                    Version = VCardVersion.vCard3_0,
                    Uid = null,
                    FormattedName = "Jane Doe",
                    Telephones =
                    [
                        new Telephone(){
                            Value = "+1 987 654 3210",
                        }
                    ]
                }
            ]
        };

        return vCard;
    }
}