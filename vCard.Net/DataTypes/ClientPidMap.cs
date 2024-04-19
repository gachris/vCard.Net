using System.IO;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents the Client PID map (CLIENTPIDMAP) property of a vCard object.
/// </summary>
/// <remarks>
/// This property class parses the <see cref="Uri"/> property to allow access to its individual property ID and URI parts.
/// This property is only valid for use with the vCard 4.0 specification.
/// </remarks>
public class ClientPidMap : EncodableDataType
{
    /// <summary>
    /// Gets or sets the property ID number.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the URI.
    /// </summary>
    public string Uri { get; set; }

    /// <summary>
    /// Gets the versions of the vCard specification supported by this property.
    /// </summary>
    /// <value>
    /// Only supported by the vCard 4.0 specification.
    /// </value>
    public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard40;

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
}