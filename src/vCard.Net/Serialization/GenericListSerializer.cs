using System.Collections;
using System.Reflection;

namespace vCard.Net.Serialization;

/// <summary>
/// Serializer for generic lists.
/// </summary>
public class GenericListSerializer : SerializerBase
{
    private readonly Type _innerType;
    private readonly Type _objectType;
    private MethodInfo _addMethodInfo;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericListSerializer"/> class.
    /// </summary>
    /// <param name="objectType">The type of the generic list.</param>
    public GenericListSerializer(Type objectType)
    {
        _innerType = objectType.GetGenericArguments()[0];

        var listDef = typeof(List<>);
        _objectType = listDef.MakeGenericType(typeof(object));
    }

    /// <inheritdoc/>
    public override Type TargetType => _objectType;

    /// <inheritdoc/>
    public override string SerializeToString(object obj) => throw new NotImplementedException();

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr)
    {
        if (SerializationContext.Peek() is not IvCardProperty p)
        {
            return null;
        }

        // Get a serializer factory to deserialize the contents of this list
        var listObj = Activator.CreateInstance(_objectType);
        if (listObj == null)
        {
            return null;
        }

        // Get a serializer for the inner type
        var sf = GetService<ISerializerFactory>();
        if (sf.Build(_innerType, SerializationContext) is not IStringSerializer stringSerializer)
        {
            return null;
        }
        // Deserialize the inner object
        var value = tr.ReadToEnd();

        // If deserialization failed, pass the string value into the list.
        var objToAdd = stringSerializer.Deserialize(new StringReader(value)) ?? value;

        // FIXME: cache this
        if (_addMethodInfo == null)
        {
            _addMethodInfo = _objectType.GetMethod("Add");
        }

        // Determine if the returned object is an IList<ObjectType>, rather than just an ObjectType.
        if (objToAdd is IList add)
        {
            //Deserialization returned an IList<ObjectType>, instead of an ObjectType.  So enumerate through the items in the list and add
            //them individually to our list.
            foreach (var innerObj in add)
            {
                _addMethodInfo.Invoke(listObj, new[] { innerObj });
            }
        }
        else
        {
            // Add the object to the list
            _addMethodInfo.Invoke(listObj, new[] { objToAdd });
        }
        return listObj;
    }
}