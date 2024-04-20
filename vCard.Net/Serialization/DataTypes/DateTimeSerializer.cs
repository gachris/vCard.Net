using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using vCard.Net.DataTypes;

namespace vCard.Net.Serialization.DataTypes;

/// <summary>
/// Serializer for vCard date-time data types.
/// </summary>
public class DateTimeSerializer : EncodableDataTypeSerializer
{
    private const RegexOptions _ciCompiled = RegexOptions.Compiled | RegexOptions.IgnoreCase;
    private static readonly Regex DateOnlyMatch = new Regex(@"^((\d{4})-(\d{2})-(\d{2}))?$", _ciCompiled);
    private static readonly Regex FullDateTimePatternMatch = new Regex(@"^((\d{4})-(\d{2})-(\d{2}))T((\d{2}):(\d{2}):(\d{2})(Z)?)$", _ciCompiled);

    /// <summary>
    /// Initializes a new instance of the <see cref="DateTimeSerializer"/> class.
    /// </summary>
    public DateTimeSerializer()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateTimeSerializer"/> class with the specified serialization context.
    /// </summary>
    /// <param name="ctx">The serialization context.</param>
    public DateTimeSerializer(SerializationContext ctx) : base(ctx) { }

    /// <inheritdoc/>
    public override Type TargetType => typeof(vCardDateTime);

    /// <inheritdoc/>
    public override string SerializeToString(object obj)
    {
        var dt = obj as IDateTime;
        if (dt == null)
        {
            return null;
        }

        // RFC 5545 3.3.5: 
        // The date with UTC time, or absolute time, is identified by a LATIN
        // CAPITAL LETTER Z suffix character, the UTC designator, appended to
        // the time value. The "TZID" property parameter MUST NOT be applied to DATE-TIME
        // properties whose time values are specified in UTC.

        var kind = dt.IsUtc
            ? DateTimeKind.Utc
            : DateTimeKind.Local;

        if (dt.IsUtc)
        {
            dt.Parameters.Remove("TZID");
        }
        else if (!string.IsNullOrWhiteSpace(dt.TzId))
        {
            dt.Parameters.Set("TZID", dt.TzId);
        }

        DateTime.SpecifyKind(dt.Value, kind);

        var value = new StringBuilder();
        value.Append($"{dt.Year:0000}-{dt.Month:00}-{dt.Day:00}");
        if (dt.HasTime)
        {
            value.Append($"T{dt.Hour:00}:{dt.Minute:00}:{dt.Second:00}");
            if (dt.IsUtc)
            {
                value.Append("Z");
            }
        }

        // Encode the value as necessary
        return Encode(dt, value.ToString());
    }

    /// <inheritdoc/>
    public override object Deserialize(TextReader tr)
    {
        var value = tr.ReadToEnd();

        var dt = CreateAndAssociate() as IDateTime;
        if (dt == null)
        {
            return null;
        }

        // Decode the value as necessary
        value = Decode(dt, value);

        var match = FullDateTimePatternMatch.Match(value);
        if (!match.Success)
        {
            match = DateOnlyMatch.Match(value);
        }

        if (!match.Success)
        {
            return null;
        }
        var now = DateTime.Now;

        var year = now.Year;
        var month = now.Month;
        var date = now.Day;
        var hour = 0;
        var minute = 0;
        var second = 0;

        if (match.Groups[1].Success)
        {
            dt.HasDate = true;
            year = Convert.ToInt32(match.Groups[2].Value);
            month = Convert.ToInt32(match.Groups[3].Value);
            date = Convert.ToInt32(match.Groups[4].Value);
        }
        if (match.Groups.Count >= 6 && match.Groups[5].Success)
        {
            dt.HasTime = true;
            hour = Convert.ToInt32(match.Groups[6].Value);
            minute = Convert.ToInt32(match.Groups[7].Value);
            second = Convert.ToInt32(match.Groups[8].Value);
        }

        var isUtc = match.Groups[9].Success;
        var kind = isUtc
            ? DateTimeKind.Utc
            : DateTimeKind.Local;

        if (isUtc)
        {
            dt.TzId = "UTC";
        }

        dt.Value = CoerceDateTime(year, month, date, hour, minute, second, kind);
        return dt;
    }

    private DateTime CoerceDateTime(int year, int month, int day, int hour, int minute, int second, DateTimeKind kind)
    {
        var dt = DateTime.MinValue;

        // NOTE: determine if a date/time value exceeds the representable date/time values in .NET.
        // If so, let's automatically adjust the date/time to compensate.
        // FIXME: should we have a parsing setting that will throw an exception
        // instead of automatically adjusting the date/time value to the
        // closest representable date/time?
        try
        {
            if (year > 9999)
            {
                dt = DateTime.MaxValue;
            }
            else if (year > 0)
            {
                dt = new DateTime(year, month, day, hour, minute, second, kind);
            }
        }
        catch { }

        return dt;
    }
}