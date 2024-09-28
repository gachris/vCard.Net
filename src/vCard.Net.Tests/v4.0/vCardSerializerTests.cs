using System.Text;
using vCard.Net.CardComponents;
using vCard.Net.DataTypes;
using vCard.Net.Serialization;
using Xunit;

namespace vCard.Net.Tests.v4_0;

public class VCardSerializerTests
{
    #region Fields/Consts

    private readonly string _dataFilePath;

    #endregion

    public VCardSerializerTests()
    {
        // Use AppContext.BaseDirectory to get the test directory
        _dataFilePath = Path.Combine(AppContext.BaseDirectory, "v4.0/Data/John Doe.vcf");
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

    private static VCard CreateCard()
    {
        var vCard = new VCard
        {
            Version = VCardVersion.vCard4_0,
            Uid = "12345678-9abc-def0-1234-56789abcdef0",
            Anniversary = new VCardDateTime(2010, 06, 15)
            {
                HasTime = false
            },
            DeathDate = new VCardDateTime(2070, 05, 31)
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
            Hobby = new Hobby()
            {
                Value = "Hiking,Photography"
            },
            Interest = new Interest()
            {
                Value = "Art,Technology"
            },
            Kind = new Kind()
            {
                CardKind = KindType.Individual
            },
            DeathPlace = new DeathPlace()
            {
                Value = "City,Country"
            },
            Expertise = new Expertise()
            {
                Value = "Python Programming,AI Development"
            },
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
            Birthdate = new VCardDateTime(1980, 1, 1)
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
                    Label = "1234 Company St\nCity, State 12345\nUSA"
                }
            ],
            Telephones =
            [
                new Telephone
                {
                    Value = "+1-234-567-8900",
                    Types = ["work", "voice"],
                    ValueType = "uri:tel"
                },
                new Telephone
                {
                    Value = "+1-234-567-8901",
                    Types = ["home", "voice"],
                    ValueType = "uri:tel"
                }
            ],
            Emails =
            [
                new Email
                {
                    Value = "johndoe@example.com"
                }
            ],
            BirthPlace = new BirthPlace()
            {
                Value = "City,Country"
            },
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
            RevisionDate = new VCardDateTime(2023, 09, 01, 12, 0, 0)
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
            Agents =
            [
                new CardComponents.VCard()
                {
                    Version = VCardVersion.vCard4_0,
                    Uid = null,
                    FormattedName = "Jane Doe",
                    Telephones =
                    [
                        new Telephone(){
                            Value = "+1-987-654-3210",
                        }
                    ]
                }
            ]
        };

        return vCard;
    }
}