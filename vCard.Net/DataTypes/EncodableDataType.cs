namespace vCard.Net.DataTypes;

/// <summary>
/// An abstract class representing an encoding of vCard data types.
/// </summary>
public class EncodableDataType : vCardDataType, IEncodableDataType
{
    /// <summary>
    /// Gets or sets the encoding of the data type.
    /// </summary>
    public virtual string Encoding
    {
        get => Parameters.Get("ENCODING");
        set => Parameters.Set("ENCODING", value);
    }

    /// <summary>
    /// Gets the versions of the vCard specification supported by this data type.
    /// </summary>
    public override SpecificationVersions VersionsSupported => SpecificationVersions.vCardAll;
}