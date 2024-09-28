using vCard.Net.CardComponents;
using vCard.Net.DataTypes;

namespace vCard.Net.Tests.v3_0;

public class VCardFixture
{
    #region Properties

    public VCard TestVCard { get; private set; }

    #endregion

    public VCardFixture()
    {
        TestVCard = CreateCard();
    }

    private static VCard CreateCard()
    {
        var vCard = new VCard
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
        };

        var agent = new Agent()
        {
            Version = VCardVersion.vCard3_0,
            Uid = "12345678-9abc-def0-1234-56789abcdef0",
            FormattedName = "Jane Doe",
            Telephones =
            [
                new Telephone(){
                    Value = "+1 987 654 3210",
                }
            ]
        };

        vCard.Agents.Add(agent);

        return vCard;
    }
}