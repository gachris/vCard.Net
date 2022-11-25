using vCard.Net.Collections.Interfaces;
using vCard.Net.DataTypes;

namespace vCard.Net
{
    public interface ICardProperty : ICardParameterCollectionContainer, ICardObject, IValueObject<object>
    {
        object Value { get; set; }
    }
}