using System.Text;
using vCard.Net.CardComponents;
using vCard.Net.DataTypes;
using vCard.Net.Serialization;

namespace vCard.Net.Tests.Serialization.DataTypes;

public class SourceSerializerTests
{
    [Theory]
    [InlineDataEx("vCards/Properties/Source/no-context.vcf", "http://directory.example.com/addressbooks/jdoe/Jean%20Dupont.vcf", null)]
    [InlineDataEx("vCards/Properties/Source/with-context.vcf", "http://directory.example.com/addressbooks/jdoe/Jean%20Dupont.vcf", "on-directory")]
    public void SerializeToString(string vCardData, string sourceUri, string? sourceContext)
    {
        var serializer = new ComponentSerializer();
        var vCard = CreateCard(sourceUri, sourceContext);
        var vCardAsString = serializer.SerializeToString(vCard);

        // Assert
        Assert.Equal(vCardData.Trim(), vCardAsString.Trim());
    }

    [Theory]
    [InlineDataEx("vCards/Properties/Source/no-context.vcf", "http://directory.example.com/addressbooks/jdoe/Jean%20Dupont.vcf", null)]
    [InlineDataEx("vCards/Properties/Source/with-context.vcf", "http://directory.example.com/addressbooks/jdoe/Jean%20Dupont.vcf", "on-directory")]
    public void Deserialize(string vCardData, string sourceUri, string? sourceContext)
    {
        var vCard = CreateCard(sourceUri, sourceContext);
        var bytes = Encoding.UTF8.GetBytes(vCardData);
        using var stream = new MemoryStream(bytes);
        var vCardFromData = (CardComponents.vCard)SimpleDeserializer.Default.Deserialize(new StreamReader(stream, Encoding.UTF8)).First<IvCardComponent>();

        // Assert
        Assert.Equal(vCard, vCardFromData);
        Assert.Equal(vCard.Source, vCardFromData.Source);
    }

    private static CardComponents.vCard CreateCard(string uri, string? context)
    {
        return new CardComponents.vCard
        {
            Version = vCardVersion.vCard30,
            Uid = "e6c4bcf4-5a19-4a1e-bfab-386e8be095f7",
            Source = new Source()
            {
                Value = new Uri(uri),
                Context = context
            }
        };
    }
}