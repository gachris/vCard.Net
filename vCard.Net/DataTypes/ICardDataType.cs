using System;

namespace vCard.Net.DataTypes
{
    public interface ICardDataType : ICardParameterCollectionContainer, ICopyable, IServiceProvider
    {
        Type GetValueType();
        void SetValueType(string type);
        ICardObject AssociatedObject { get; set; }
        string Language { get; set; }
    }
}