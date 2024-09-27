using System;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization;

internal static class vCardVersionExtensions
{
    public static string ToVersionString(this vCardVersion version)
    {
        return version switch
        {
            vCardVersion.vCard2_1 => "2.1",
            vCardVersion.vCard3_0 => "3.0",
            vCardVersion.vCard4_0 => "4.0",
            _ => throw new ArgumentOutOfRangeException(nameof(version), version, null),
        };
    }

    public static vCardVersion FromVersionString(this string versionString)
    {
        return versionString switch
        {
            "2.1" => vCardVersion.vCard2_1,
            "3.0" => vCardVersion.vCard3_0,
            "4.0" => vCardVersion.vCard4_0,
            _ => throw new ArgumentException($"Unsupported vCard version string: {versionString}", nameof(versionString)),
        };
    }
}