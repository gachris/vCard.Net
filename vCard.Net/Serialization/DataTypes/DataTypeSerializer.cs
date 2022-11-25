using System;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes
{
    public abstract class DataTypeSerializer : SerializerBase
    {
        protected DataTypeSerializer() { }

        protected DataTypeSerializer(SerializationContext ctx) : base(ctx) { }

        protected virtual ICardDataType CreateAndAssociate()
        {
            // Create an instance of the object
            if (!(Activator.CreateInstance(TargetType) is ICardDataType dt))
            {
                return null;
            }

            if (SerializationContext.Peek() is ICardObject associatedObject)
            {
                dt.AssociatedObject = associatedObject;
            }

            return dt;
        }
    }
}