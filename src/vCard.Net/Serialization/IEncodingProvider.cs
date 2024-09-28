namespace vCard.Net.Serialization;

/// <summary>
/// Interface for encoding providers.
/// </summary>
internal interface IEncodingProvider
{
    /// <summary>
    /// Encodes the given data using the specified encoding.
    /// </summary>
    /// <param name="encoding">The encoding to use.</param>
    /// <param name="data">The data to encode.</param>
    /// <returns>The encoded data as a string.</returns>
    string Encode(string encoding, byte[] data);

    /// <summary>
    /// Decodes the given string using the specified encoding.
    /// </summary>
    /// <param name="encoding">The encoding to use.</param>
    /// <param name="value">The string to decode.</param>
    /// <returns>The decoded string.</returns>
    string DecodeString(string encoding, string value);

    /// <summary>
    /// Decodes the given string data using the specified encoding.
    /// </summary>
    /// <param name="encoding">The encoding to use.</param>
    /// <param name="value">The string data to decode.</param>
    /// <returns>The decoded data as a byte array.</returns>
    byte[] DecodeData(string encoding, string value);
}