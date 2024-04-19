using System.IO;
using vCard.Net.Serialization.DataTypes;

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
    /// Gets the specification versions supported by the Key object.
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
}