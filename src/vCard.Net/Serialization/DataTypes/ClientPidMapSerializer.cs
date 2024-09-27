using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for ClientPidMap data type, providing serialization and deserialization methods.
/// </summary>
public class ClientPidMapSerializer : StringSerializer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ClientPidMapSerializer"/> class.
    /// </summary>
    public ClientPidMapSerializer() : base() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientPidMapSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public ClientPidMapSerializer(SerializationContext ctx) : base(ctx)
    {
    }

    /// <inheritdoc/>
    public override Type TargetType => typeof(ClientPidMap);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        return obj is not ClientPidMap clientPidMap
            ? null
            : clientPidMap.Id == 0 || string.IsNullOrWhiteSpace(clientPidMap.Uri)
            ? null
            : Encode(clientPidMap, string.Join(";", clientPidMap.Id.ToString(), clientPidMap.Uri));
    }

    /// <summary>
    /// Deserializes a string representation of a <see cref="ClientPidMap"/>.
    /// </summary>
    /// <param name="value">The string representation of the <see cref="ClientPidMap"/>.</param>
    /// <returns>The deserialized <see cref="ClientPidMap"/>.</returns>
    public ClientPidMap Deserialize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (CreateAndAssociate() is not ClientPidMap clientPidMap)
        {
            return null;
        }

        // Decode the value, if necessary!
        value = Decode(clientPidMap, value);

        if (value == null || value.Length <= 0)
        {
            return null;
        }

        clientPidMap.Id = 0;
        clientPidMap.Uri = null;
        if (!string.IsNullOrWhiteSpace(value))
        {
            string[] array = value.Split(new char[1] { ';' });
            if (array[0].Length != 0 && int.TryParse(array[0], out var result) && result != 0)
            {
                clientPidMap.Id = result;
            }

            if (array.Length > 1)
            {
                clientPidMap.Uri = array[1];
            }
        }

        return clientPidMap;
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr) => Deserialize(tr.ReadToEnd());
}