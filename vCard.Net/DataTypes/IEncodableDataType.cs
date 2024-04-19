namespace vCard.Net.DataTypes;

/// <summary>
/// Represents a data type that can be encoded.
/// </summary>
public interface IEncodableDataType
{
    /// <summary>
    /// Gets or sets the encoding of the data type.
    /// </summary>
    string Encoding { get; set; }
}
