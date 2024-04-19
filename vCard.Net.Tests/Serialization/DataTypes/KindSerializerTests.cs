using System.Text;
using vCard.Net.CardComponents;
using vCard.Net.DataTypes;
using vCard.Net.Serialization;

namespace vCard.Net.Tests.Serialization.DataTypes;

public class KindSerializerTests
{
    [Theory]
    [InlineDataEx("vCards/Properties/Kind/none.vcf", KindType.None)]
    [InlineDataEx("vCards/Properties/Kind/group.vcf", KindType.Group)]
    [InlineDataEx("vCards/Properties/Kind/individual.vcf", KindType.Individual)]
    [InlineDataEx("vCards/Properties/Kind/org.vcf", KindType.Organization)]
    [InlineDataEx("vCards/Properties/Kind/location.vcf", KindType.Location)]
    [InlineDataEx("vCards/Properties/Kind/other.vcf", "other-kind")]
    public void SerializeToString(string vCardData, object cardKind)
    {
        var serializer = new ComponentSerializer();
        var vCard = CreateCard(cardKind);
        var vCardAsString = serializer.SerializeToString(vCard).Trim();

        // Assert
        Assert.Equal(vCardData.Trim(), vCardAsString);
    }

    [Theory]
    [InlineDataEx("vCards/Properties/Kind/none.vcf", KindType.None)]
    [InlineDataEx("vCards/Properties/Kind/group.vcf", KindType.Group)]
    [InlineDataEx("vCards/Properties/Kind/individual.vcf", KindType.Individual)]
    [InlineDataEx("vCards/Properties/Kind/org.vcf", KindType.Organization)]
    [InlineDataEx("vCards/Properties/Kind/location.vcf", KindType.Location)]
    [InlineDataEx("vCards/Properties/Kind/other.vcf", "other-kind")]
    public void Deserialize(string vCardData, object cardKind)
    {
        var vCard = CreateCard(cardKind);
        var bytes = Encoding.UTF8.GetBytes(vCardData);
        using var stream = new MemoryStream(bytes);
        var vCardFromData = (CardComponents.vCard)SimpleDeserializer.Default.Deserialize(new StreamReader(stream, Encoding.UTF8)).First<IvCardComponent>();

        // Assert
        Assert.Equal(vCard, vCardFromData);
        Assert.Equal(vCard.Kind, vCardFromData.Kind);
    }

    private static CardComponents.vCard CreateCard(object value)
    {
        var vCard = new CardComponents.vCard
        {
            Version = vCardVersion.vCard30,
            Uid = "e6c4bcf4-5a19-4a1e-bfab-386e8be095f7",
            Kind = new Kind()
        };

        if (value is KindType cardKind)
        {
            vCard.Kind.CardKind = cardKind;
        }
        else if (value is string otherKind)
        {
            vCard.Kind.OtherKind = otherKind;
        }

        return vCard;
    }
}