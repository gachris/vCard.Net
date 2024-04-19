using System;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization;

internal static class vCardVersionExtensions
{
    public static string ToVersionString(this vCardVersion version)
    {
        switch (version)
        {
            case vCardVersion.vCard21:
                return "2.1";
            case vCardVersion.vCard30:
                return "3.0";
            case vCardVersion.vCard40:
                return "4.0";
            default:
                throw new ArgumentOutOfRangeException(nameof(version), version, null);
        }
    }
}