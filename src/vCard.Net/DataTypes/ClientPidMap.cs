using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents the Client PID map (CLIENTPIDMAP) property of a vCard object.
/// </summary>
/// <remarks>
/// This property class parses the <see cref="Value"/> property to allow access to its individual property ID and URI parts.
/// This property is only valid for use with the vCard 4.0 specification.
/// </remarks>
public class ClientPidMap : EncodableDataType
{
    /// <summary>
    /// Gets or sets the property ID number.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientPidMap"/> class.
    /// </summary>
    public ClientPidMap()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientPidMap"/> class with the specified value.
    /// </summary>
    /// <param name="value">The value of the client PID map.</param>
    public ClientPidMap(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new ClientPidMapSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }

    /// <summary>
    /// Determines whether the current <see cref="ClientPidMap"/> object is equal to another <see cref="ClientPidMap"/> object.
    /// </summary>
    /// <param name="other">The <see cref="ClientPidMap"/> object to compare with the current object.</param>
    /// <returns>True if the current object is equal to the other object; otherwise, false.</returns>
    protected bool Equals(ClientPidMap other)
    {
        return Id == other.Id && string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj != null && (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((ClientPidMap)obj));
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            var hashCode = 17;
            hashCode = hashCode * 23 + Id.GetHashCode();
            hashCode = hashCode * 23 + (Value != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Value) : 0);
            return hashCode;
        }
    }
}