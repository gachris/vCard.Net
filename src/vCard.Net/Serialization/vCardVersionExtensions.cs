using vCard.Net.DataTypes;

namespace vCard.Net.Serialization;

internal static class VCardVersionExtensions
{
    public static string ToVersionString(this VCardVersion version)
    {
        return version switch
        {
            VCardVersion.vCard2_1 => "2.1",
            VCardVersion.vCard3_0 => "3.0",
            VCardVersion.vCard4_0 => "4.0",
            _ => throw new ArgumentOutOfRangeException(nameof(version), version, null),
        };
    }

    public static VCardVersion FromVersionString(this string versionString)
    {
        return versionString switch
        {
            "2.1" => VCardVersion.vCard2_1,
            "3.0" => VCardVersion.vCard3_0,
            "4.0" => VCardVersion.vCard4_0,
            _ => throw new ArgumentException($"Unsupported vCard version string: {versionString}", nameof(versionString)),
        };
    }
}