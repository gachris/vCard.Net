using System;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Abstract base class for serializers of vCard data types.
/// </summary>
public abstract class DataTypeSerializer : SerializerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataTypeSerializer"/> class.
    /// </summary>
    protected DataTypeSerializer() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataTypeSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    protected DataTypeSerializer(SerializationContext ctx) : base(ctx) { }

    /// <summary>
    /// Creates a new instance of the target data type and associates it with the current serialization context.
    /// </summary>
    /// <returns>The newly created data type object.</returns>
    protected virtual IvCardDataType CreateAndAssociate()
    {
        // Create an instance of the object
        if (Activator.CreateInstance(TargetType) is not IvCardDataType dt)
        {
            return null;
        }

        if (SerializationContext.Peek() is IvCardObject associatedObject)
        {
            dt.AssociatedObject = associatedObject;
        }

        return dt;
    }
}