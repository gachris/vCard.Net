using System.Text;
using vCard.Net.DataTypes;
using vCard.Net.Serialization;

namespace vCard.Net.Tests.Serialization.DataTypes;

public class PhoneSerializerTests
{
    [Theory]
    [InlineDataEx("vCards/Properties/Phone/work_voice.vcf", new string[] { "WORK", "VOICE" }, "(111) 555-1212")]
    public void SerializeToString(string vCardData, IEnumerable<string> types, string value)
    {
        var serializer = new ComponentSerializer();
        var vCard = CreateCard(types, value);
        var vCardAsString = serializer.SerializeToString(vCard).Trim();

        // Assert
        Assert.Equal(vCardData.Trim(), vCardAsString);
    }

    [Theory]
    [InlineDataEx("vCards/Properties/Phone/work_voice.vcf", new string[] { "WORK", "VOICE" }, "(111) 555-1212")]
    public void Deserialize(string vCardData, IEnumerable<string> types, string value)
    {
        var vCard = CreateCard(types, value);
        var bytes = Encoding.UTF8.GetBytes(vCardData);
        using var stream = new MemoryStream(bytes);
        var vCardFromData = (CardComponents.vCard)SimpleDeserializer.Default.Deserialize(new StreamReader(stream, Encoding.UTF8)).First();

        // Assert
        Assert.Equal(vCard, vCardFromData);
    }

    private static CardComponents.vCard CreateCard(IEnumerable<string> types, string value)
    {
        var vCard = new CardComponents.vCard
        {
            Version = vCardVersion.vCard30,
            Uid = "e6c4bcf4-5a19-4a1e-bfab-386e8be095f7",
        };

        var phone = new PhoneNumber()
        {
            Value = value,
            AssociatedObject = vCard
        };

        foreach (var type in types)
        {
            phone.Types.Add(type);
        }

        vCard.PhoneNumbers.Add(phone);

        return vCard;
    }
}