using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents the Source (SOURCE) property of a vCard.
/// </summary>
/// <remarks>
/// A source identifies a directory that contains or provided information for the
/// vCard. A source consists of a URI and a context. The URI is generally a URL;
/// the context identifies the protocol and type of URI. For example, a vCard associated
/// with an LDAP directory entry will have an ldap:// URL and a context of "LDAP".
/// </remarks>
public class Source : EncodableDataType
{
    /// <summary>
    /// Gets the versions of the vCard specification supported by this property.
    /// </summary>
    /// <value>
    /// Supports all vCard specifications.
    /// </value>
    public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;

    /// <summary>
    /// Gets or sets a string containing the context of the property value such as a protocol for a URI.
    /// </summary>
    /// <value>
    /// The value is a string defining the context of the property value such as LDAP,
    /// HTTP, etc. It is only applicable to vCard 3.0 objects.
    /// </value>
    public string Context
    {
        get => Parameters.Get("CONTEXT");
        set
        {
            if (value is null)
            {
                Parameters.Remove("CONTEXT");
            }
            else Parameters.Set("CONTEXT", value);
        }
    }

    /// <summary>
    /// Gets or sets the URI value.
    /// </summary>
    public Uri Value { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Source"/> class.
    /// </summary>
    public Source()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Source"/> class with the specified value.
    /// </summary>
    /// <param name="value">The value of the source.</param>
    public Source(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new SourceSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }

    /// <summary>
    /// Determines whether the current <see cref="Source"/> object is equal to another <see cref="Source"/> object.
    /// </summary>
    /// <param name="other">The <see cref="Source"/> object to compare with the current object.</param>
    /// <returns>True if the current object is equal to the other object; otherwise, false.</returns>
    protected bool Equals(Source other)
    {
        return string.Equals(Context, other.Context, StringComparison.OrdinalIgnoreCase) && Equals(Value, other.Value);
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj is not null && (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Source)obj));
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Context?.GetHashCode() ?? 0;
            hashCode = hashCode * 397 ^ (Value?.GetHashCode() ?? 0);
            return hashCode;
        }
    }
}
