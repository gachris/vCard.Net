using System.Text;
using vCard.Net.CardComponents;
using vCard.Net.Serialization;
using Xunit;

namespace vCard.Net.Tests.v2_1;

public class VCardSerializerTests : IClassFixture<VCardFixture>
{
    #region Fields/Consts

    private readonly string _dataFilePath;
    private readonly VCardFixture _fixture;

    #endregion

    public VCardSerializerTests(VCardFixture fixture)
    {
        _fixture = fixture;
        _dataFilePath = Path.Combine(AppContext.BaseDirectory, "v2.1/Data/John Doe.vcf");
    }

    [Fact]
    public void SerializeToString_Success()
    {
        Assert.True(File.Exists(_dataFilePath), $"File not found: {_dataFilePath}");

        // Read vCard from file
        var vCardData = File.ReadAllText(_dataFilePath);

        // Serialize vCard object
        var serializer = new ComponentSerializer();
        var vCardAsString = serializer.SerializeToString(_fixture.TestVCard);

        // Check if serialized string matches expected file content
        Assert.Equal(vCardData, vCardAsString);
    }

    [Fact]
    public void Deserialize_Success()
    {
        Assert.True(File.Exists(_dataFilePath), $"File not found: {_dataFilePath}");

        // Read the vCard data from file
        var vCardData = File.ReadAllText(_dataFilePath);

        // Deserialize vCard data
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(vCardData));
        using var reader = new StreamReader(stream, Encoding.UTF8);
        var deserializedCard = (VCard)SimpleDeserializer.Default.Deserialize(reader).First();

        // Check individual properties
        Assert.Equal(_fixture.TestVCard.Uid, deserializedCard.Uid);
        Assert.Equal(_fixture.TestVCard.FormattedName, deserializedCard.FormattedName);
        Assert.Equal(_fixture.TestVCard.N.GivenName, deserializedCard.N.GivenName);
        Assert.Equal(_fixture.TestVCard.N.FamilyName, deserializedCard.N.FamilyName);

        // Assert full object equality last (after checking important fields)
        Assert.Equal(_fixture.TestVCard, deserializedCard);
    }
}