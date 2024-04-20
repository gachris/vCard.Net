using System;
using System.IO;
using vCard.Net.Serialization.DataTypes;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents the kind (KIND) property of a vCard. This specifies the type of entity represented by the vCard.
/// </summary>
public class Kind : EncodableDataType
{
    private KindType _cardKind;
    private string _otherKind;

    /// <summary>
    /// Gets the versions of the vCard specification supported by this property.
    /// </summary>
    /// <value>
    /// Only supported by the vCard 4.0 specification.
    /// </value>
    public override SpecificationVersions VersionsSupported => SpecificationVersions.vCard40;

    /// <summary>
    /// Gets or sets the vCard kind value.
    /// </summary>
    /// <remarks>
    /// Setting this parameter to Other sets the <see cref="OtherKind"/> to "X-UNKNOWN" if not already set to something else.
    /// </remarks>
    public KindType CardKind
    {
        get => _cardKind;
        set
        {
            _cardKind = value;
            if (_cardKind != KindType.Other)
            {
                _otherKind = null;
            }
            else if (string.IsNullOrWhiteSpace(_otherKind))
            {
                _otherKind = "X-UNKNOWN";
            }
        }
    }

    /// <summary>
    /// Gets or sets the card kind string when the type is set to Other.
    /// </summary>
    /// <remarks>
    /// Setting this parameter automatically sets the <see cref="CardKind"/> property to Other.
    /// </remarks>
    public string OtherKind
    {
        get => _otherKind;
        set
        {
            _cardKind = KindType.Other;
            _otherKind = !string.IsNullOrWhiteSpace(value) ? value : "X-UNKNOWN";
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Kind"/> class.
    /// </summary>
    public Kind()
    {
        _cardKind = KindType.None;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Kind"/> class with the specified value.
    /// </summary>
    /// <param name="value">The kind value as a string.</param>
    public Kind(string value)
    {
        _cardKind = KindType.None;

        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }

        var serializer = new KindSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }

    /// <summary>
    /// Determines whether the current <see cref="Kind"/> object is equal to another <see cref="Kind"/> object.
    /// </summary>
    /// <param name="other">The <see cref="Kind"/> object to compare with the current object.</param>
    /// <returns>True if the current object is equal to the other object; otherwise, false.</returns>
    protected bool Equals(Kind other)
    {
        return Equals(_cardKind, other._cardKind) && string.Equals(_otherKind, other._otherKind, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return obj != null && (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Kind)obj));
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = _cardKind.GetHashCode();
            hashCode = hashCode * 397 ^ (_otherKind?.GetHashCode() ?? 0);
            return hashCode;
        }
    }
}