using System.Text;
using vCard.Net.CardComponents;
using vCard.Net.DataTypes;
using vCard.Net.Serialization;

namespace vCard.Net.Tests.Serialization.DataTypes
{
    public class KindSerializerTests
    {
        [Theory]
        [InlineDataEx("vCards/Properties/Kind/none.vcf", CardKind.None)]
        [InlineDataEx("vCards/Properties/Kind/group.vcf", CardKind.Group)]
        [InlineDataEx("vCards/Properties/Kind/individual.vcf", CardKind.Individual)]
        [InlineDataEx("vCards/Properties/Kind/org.vcf", CardKind.Organization)]
        [InlineDataEx("vCards/Properties/Kind/location.vcf", CardKind.Location)]
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
        [InlineDataEx("vCards/Properties/Kind/none.vcf", CardKind.None)]
        [InlineDataEx("vCards/Properties/Kind/group.vcf", CardKind.Group)]
        [InlineDataEx("vCards/Properties/Kind/individual.vcf", CardKind.Individual)]
        [InlineDataEx("vCards/Properties/Kind/org.vcf", CardKind.Organization)]
        [InlineDataEx("vCards/Properties/Kind/location.vcf", CardKind.Location)]
        [InlineDataEx("vCards/Properties/Kind/other.vcf", "other-kind")]
        public void Deserialize(string vCardData, object cardKind)
        {
            var vCard = CreateCard(cardKind);
            var bytes = Encoding.UTF8.GetBytes(vCardData);
            using var stream = new MemoryStream(bytes);
            var vCardFromData = (Card)SimpleDeserializer.Default.Deserialize(new StreamReader(stream, Encoding.UTF8)).First();

            // Assert
            Assert.Equal(vCard, vCardFromData);
            Assert.Equal(vCard.Kind, vCardFromData.Kind);
        }

        private static Card CreateCard(object value)
        {
            var vCard = new Card
            {
                Uid = "e6c4bcf4-5a19-4a1e-bfab-386e8be095f7",
                Kind = new Kind()
            };

            if (value is CardKind cardKind)
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
}