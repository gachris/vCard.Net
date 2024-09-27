using vCard.Net.Serialization.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents the Public Key (KEY) property of a vCard.
/// </summary>
/// <remarks>
/// The <see cref="Value"/> property contains the key value
/// in string form. There is limited support for this property. It will decode the
/// key type parameter and make it accessible through the <see cref="KeyType"/> property.
/// </remarks>
public class Key : EncodableDataType
{
    /// <summary>
    /// Gets the versions of the vCard specification supported by this property.
    /// </summary>
    /// <value>
    /// Supports all vCard specifications.
    /// </value>
    public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;

    /// <summary>
    /// Gets or sets the public key type.
    /// </summary>
    /// <value>
    /// The value is a string defining the type of key that the property value represents such as X509, PGP, etc.
    /// </value>
    public string KeyType
    {
        get => Parameters.Get("TYPE");
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                Parameters.Set("TYPE", value);
            }
            else
            {
                Parameters.Remove("TYPE");
            }
        }
    }

    /// <summary>
    /// Gets or sets the value of the public key.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Key"/> class.
    /// </summary>
    public Key()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Key"/> class with the specified value.
    /// </summary>
    /// <param name="value">The public key value.</param>
    public Key(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new KeySerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }

    /// <summary>
    /// Determines whether the current <see cref="Key"/> object is equal to another <see cref="Key"/> object.
    /// </summary>
    /// <param name="other">The <see cref="Key"/> object to compare with the current object.</param>
    /// <returns>True if the current object is equal to the other object; otherwise, false.</returns>
    protected bool Equals(Key other)
    {
        return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase)
               && CollectionHelpers.Equals(KeyType, other.KeyType);
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj != null && (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Key)obj));
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            var hashCode = 17;
            hashCode = hashCode * 23 + (Value != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(Value) : 0);
            hashCode = (hashCode * 23) ^ CollectionHelpers.GetHashCode(KeyType);
            return hashCode;
        }
    }
}