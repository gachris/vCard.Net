using NodaTime;
using System;
using System.IO;
using vCard.Net.Serialization.DataTypes;
using vCard.Net.Utility;

namespace vCard.Net.DataTypes;

/// <summary>
/// Represents a date and time value in a vCard object.
/// </summary>
/// <remarks>
/// <para>
/// This class serves as the vCard equivalent of the .NET <see cref="DateTime"/> class. In addition to the features
/// of the <see cref="DateTime"/> class, the <see cref="vCardDateTime"/> class handles time zone differences and integrates
/// seamlessly into the vCard framework.
/// </para>
/// </remarks>
public sealed class vCardDateTime : EncodableDataType, IDateTime
{
    /// <summary>
    /// Gets the current system date and time as a <see cref="vCardDateTime"/> object.
    /// </summary>
    public static vCardDateTime Now => new(DateTime.Now);

    /// <summary>
    /// Gets the current date as a <see cref="vCardDateTime"/> object.
    /// </summary>
    public static vCardDateTime Today => new(DateTime.Today);

    private bool _hasDate;
    private bool _hasTime;

    /// <summary>
    /// Initializes a new instance of the <see cref="vCardDateTime"/> class.
    /// </summary>
    public vCardDateTime()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="vCardDateTime"/> class with the specified <see cref="IDateTime"/> value.
    /// </summary>
    /// <param name="value">The <see cref="IDateTime"/> value.</param>
    public vCardDateTime(IDateTime value)
    {
        Initialize(value.Value, value.TzId);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="vCardDateTime"/> class with the specified <see cref="DateTime"/> value.
    /// </summary>
    /// <param name="value">The <see cref="DateTime"/> value.</param>
    public vCardDateTime(DateTime value) : this(value, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="vCardDateTime"/> class with the specified <see cref="DateTime"/> value and time zone ID.
    /// </summary>
    /// <param name="value">The <see cref="DateTime"/> value.</param>
    /// <param name="tzId">The time zone ID.</param>
    public vCardDateTime(DateTime value, string tzId)
    {
        Initialize(value, tzId);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="vCardDateTime"/> class with the specified year, month, day, hour, minute, and second values.
    /// </summary>
    /// <param name="year">The year.</param>
    /// <param name="month">The month (1 through 12).</param>
    /// <param name="day">The day (1 through the number of days in <paramref name="month"/>).</param>
    /// <param name="hour">The hour (0 through 23).</param>
    /// <param name="minute">The minute (0 through 59).</param>
    /// <param name="second">The second (0 through 59).</param>
    public vCardDateTime(int year, int month, int day, int hour, int minute, int second)
    {
        Initialize(year, month, day, hour, minute, second, null);
        HasTime = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="vCardDateTime"/> class with the specified year, month, day, hour, minute, second, and time zone ID values.
    /// </summary>
    /// <param name="year">The year.</param>
    /// <param name="month">The month (1 through 12).</param>
    /// <param name="day">The day (1 through the number of days in <paramref name="month"/>).</param>
    /// <param name="hour">The hour (0 through 23).</param>
    /// <param name="minute">The minute (0 through 59).</param>
    /// <param name="second">The second (0 through 59).</param>
    /// <param name="tzId">The time zone ID.</param>
    public vCardDateTime(int year, int month, int day, int hour, int minute, int second, string tzId)
    {
        Initialize(year, month, day, hour, minute, second, tzId);
        HasTime = true;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="vCardDateTime"/> class with the specified year, month, and day values.
    /// </summary>
    /// <param name="year">The year.</param>
    /// <param name="month">The month (1 through 12).</param>
    /// <param name="day">The day (1 through the number of days in <paramref name="month"/>).</param>
    public vCardDateTime(int year, int month, int day) : this(year, month, day, 0, 0, 0)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="vCardDateTime"/> class with the specified year, month, day, and time zone ID values.
    /// </summary>
    /// <param name="year">The year.</param>
    /// <param name="month">The month (1 through 12).</param>
    /// <param name="day">The day (1 through the number of days in <paramref name="month"/>).</param>
    /// <param name="tzId">The time zone ID.</param>
    public vCardDateTime(int year, int month, int day, string tzId) : this(year, month, day, 0, 0, 0, tzId)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="vCardDateTime"/> class with the specified string representation of a date and time.
    /// </summary>
    /// <param name="value">A string representation of a date and time.</param>
    public vCardDateTime(string value)
    {
        var serializer = new DateTimeSerializer();
        CopyFrom(serializer.Deserialize(new StringReader(value)) as ICopyable);
    }

    private void Initialize(int year, int month, int day, int hour, int minute, int second, string tzId)
    {
        Initialize(CoerceDateTime(year, month, day, hour, minute, second, DateTimeKind.Local), tzId);
    }

    private void Initialize(DateTime value, string tzId)
    {
        if (!string.IsNullOrWhiteSpace(tzId) && !tzId.Equals("UTC", StringComparison.OrdinalIgnoreCase))
        {
            // Definitely local
            value = DateTime.SpecifyKind(value, DateTimeKind.Local);
            TzId = tzId;
        }
        else if (string.Equals("UTC", tzId, StringComparison.OrdinalIgnoreCase) || value.Kind == DateTimeKind.Utc)
        {
            // Probably UTC
            value = DateTime.SpecifyKind(value, DateTimeKind.Utc);
            TzId = "UTC";
        }

        Value = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second, value.Kind);
        HasDate = true;
        HasTime = value.Second != 0 || value.Minute != 0 || value.Hour != 0;
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

    /// <inheritdoc/>
    public override IvCardObject AssociatedObject
    {
        get => base.AssociatedObject;
        set
        {
            if (!Equals(AssociatedObject, value))
            {
                base.AssociatedObject = value;
            }
        }
    }

    /// <inheritdoc/>
    public override void CopyFrom(ICopyable obj)
    {
        base.CopyFrom(obj);

        if (obj is not IDateTime dt)
        {
            return;
        }

        _value = dt.Value;
        _hasDate = dt.HasDate;
        _hasTime = dt.HasTime;

        AssociateWith(dt);
    }

    /// <summary>
    /// Determines whether the current <see cref="vCardDateTime"/> object is equal to another <see cref="vCardDateTime"/> object.
    /// </summary>
    /// <param name="other">The <see cref="vCardDateTime"/> object to compare with the current object.</param>
    /// <returns><see langword="true"/> if the current object is equal to the <paramref name="other"/> parameter; otherwise, <see langword="false"/>.</returns>
    public bool Equals(vCardDateTime other) => this == other;

    /// <inheritdoc/>
    public override bool Equals(object other) => other is IDateTime && (vCardDateTime)other == this;

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Value.GetHashCode();
            hashCode = hashCode * 397 ^ HasDate.GetHashCode();
            hashCode = hashCode * 397 ^ AsUtc.GetHashCode();
            hashCode = hashCode * 397 ^ (TzId != null ? TzId.GetHashCode() : 0);
            return hashCode;
        }
    }

    /// <summary>
    /// Determines whether one <see cref="vCardDateTime"/> object is less than another <see cref="IDateTime"/> object.
    /// </summary>
    /// <param name="left">The first <see cref="vCardDateTime"/> object to compare.</param>
    /// <param name="right">The second <see cref="IDateTime"/> object to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator <(vCardDateTime left, IDateTime right)
    {
        return left != null && right != null && left.AsUtc < right.AsUtc;
    }

    /// <summary>
    /// Determines whether one <see cref="vCardDateTime"/> object is greater than another <see cref="IDateTime"/> object.
    /// </summary>
    /// <param name="left">The first <see cref="vCardDateTime"/> object to compare.</param>
    /// <param name="right">The second <see cref="IDateTime"/> object to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator >(vCardDateTime left, IDateTime right)
    {
        return left != null && right != null && left.AsUtc > right.AsUtc;
    }

    /// <summary>
    /// Determines whether one <see cref="vCardDateTime"/> object is less than or equal to another <see cref="IDateTime"/> object.
    /// </summary>
    /// <param name="left">The first <see cref="vCardDateTime"/> object to compare.</param>
    /// <param name="right">The second <see cref="IDateTime"/> object to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator <=(vCardDateTime left, IDateTime right)
    {
        return left != null && right != null && left.AsUtc <= right.AsUtc;
    }

    /// <summary>
    /// Determines whether one <see cref="vCardDateTime"/> object is greater than or equal to another <see cref="IDateTime"/> object.
    /// </summary>
    /// <param name="left">The first <see cref="vCardDateTime"/> object to compare.</param>
    /// <param name="right">The second <see cref="IDateTime"/> object to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator >=(vCardDateTime left, IDateTime right)
    {
        return left != null && right != null && left.AsUtc >= right.AsUtc;
    }

    /// <summary>
    /// Determines whether two specified <see cref="vCardDateTime"/> objects are equal.
    /// </summary>
    /// <param name="left">The first <see cref="vCardDateTime"/> object to compare.</param>
    /// <param name="right">The second <see cref="IDateTime"/> object to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(vCardDateTime left, IDateTime right)
    {
        return left is null || right is null ? ReferenceEquals(left, right)
              : right is vCardDateTime
              && left.Value.Equals(right.Value)
              && left.HasDate == right.HasDate
              && left.AsUtc.Equals(right.AsUtc)
              && string.Equals(left.TzId, right.TzId, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Determines whether two specified <see cref="vCardDateTime"/> objects are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="vCardDateTime"/> object to compare.</param>
    /// <param name="right">The second <see cref="IDateTime"/> object to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> and <paramref name="right"/> are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(vCardDateTime left, IDateTime right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Subtracts a specified <see cref="IDateTime"/> from a <see cref="vCardDateTime"/>.
    /// </summary>
    /// <param name="left">The <see cref="vCardDateTime"/> object to subtract from.</param>
    /// <param name="right">The <see cref="IDateTime"/> object to subtract.</param>
    /// <returns>The time interval between <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static TimeSpan operator -(vCardDateTime left, IDateTime right)
    {
        left.AssociateWith(right);
        return left.AsUtc - right.AsUtc;
    }

    /// <summary>
    /// Subtracts a specified <see cref="TimeSpan"/> from a <see cref="vCardDateTime"/>.
    /// </summary>
    /// <param name="left">The <see cref="vCardDateTime"/> object to subtract from.</param>
    /// <param name="right">The <see cref="TimeSpan"/> to subtract.</param>
    /// <returns>A new <see cref="vCardDateTime"/> object that represents the value of <paramref name="left"/> minus <paramref name="right"/>.</returns>
    public static IDateTime operator -(vCardDateTime left, TimeSpan right)
    {
        var copy = left.Copy<IDateTime>();
        copy.Value -= right;
        return copy;
    }

    /// <summary>
    /// Adds a specified <see cref="TimeSpan"/> to a <see cref="vCardDateTime"/>.
    /// </summary>
    /// <param name="left">The <see cref="vCardDateTime"/> object to add to.</param>
    /// <param name="right">The <see cref="TimeSpan"/> to add.</param>
    /// <returns>A new <see cref="vCardDateTime"/> object that represents the value of <paramref name="left"/> plus <paramref name="right"/>.</returns>
    public static IDateTime operator +(vCardDateTime left, TimeSpan right)
    {
        var copy = left.Copy<IDateTime>();
        copy.Value += right;
        return copy;
    }

    /// <summary>
    /// Implicitly converts a <see cref="DateTime"/> object to a <see cref="vCardDateTime"/>.
    /// </summary>
    /// <param name="left">The <see cref="DateTime"/> object to convert.</param>
    /// <returns>A new <see cref="vCardDateTime"/> object representing the value of <paramref name="left"/>.</returns>
    public static implicit operator vCardDateTime(DateTime left) => new(left);

    /// <summary>
    /// Converts the date/time to the date/time of the computer running the program. If the DateTimeKind is Unspecified, it's assumed that the underlying
    /// Value already represents the system's datetime.
    /// </summary>
    public DateTime AsSystemLocal
    {
        get
        {
            if (Value.Kind == DateTimeKind.Unspecified)
            {
                return HasTime
                ? Value
                    : Value.Date;
            }

            return HasTime
            ? Value.ToLocalTime()
            : Value.ToLocalTime().Date;
        }
    }

    private DateTime _asUtc = DateTime.MinValue;

    /// <inheritdoc/>
    public DateTime AsUtc
    {
        get
        {
            if (_asUtc == DateTime.MinValue)
            {
                // In order of weighting:
                //  1) Specified TzId
                //  2) Value having a DateTimeKind.Utc
                //  3) Use the OS's time zone

                if (!string.IsNullOrWhiteSpace(TzId))
                {
                    var asLocal = DateUtil.ToZonedDateTimeLeniently(Value, TzId);
                    _asUtc = asLocal.ToDateTimeUtc();
                }
                else if (IsUtc || Value.Kind == DateTimeKind.Utc)
                {
                    _asUtc = DateTime.SpecifyKind(Value, DateTimeKind.Utc);
                }
                else
                {
                    _asUtc = DateTime.SpecifyKind(Value, DateTimeKind.Local).ToUniversalTime();
                }
            }
            return _asUtc;
        }
    }

    private DateTime _value;
    /// <inheritdoc/>
    public DateTime Value
    {
        get => _value;
        set
        {
            if (_value == value && _value.Kind == value.Kind)
            {
                return;
            }

            _asUtc = DateTime.MinValue;
            _value = value;
        }
    }

    /// <inheritdoc/>
    public bool IsUtc => _value.Kind == DateTimeKind.Utc;

    /// <inheritdoc/>
    public bool HasDate
    {
        get => _hasDate;
        set => _hasDate = value;
    }

    /// <inheritdoc/>
    public bool HasTime
    {
        get => _hasTime;
        set => _hasTime = value;
    }

    private string _tzId = string.Empty;

    /// <summary>
    /// Setting the TzId to a local time zone will set Value.Kind to Local. Setting TzId to UTC will set Value.Kind to Utc. If the incoming value is null
    /// or whitespace, Value.Kind will be set to Unspecified. Setting the TzId will NOT incur a UTC offset conversion under any circumstances. To convert
    /// to another time zone, use the ToTimeZone() method.
    /// </summary>
    public string TzId
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_tzId))
            {
                _tzId = Parameters.Get("TZID");
            }
            return _tzId;
        }
        set
        {
            if (string.Equals(_tzId, value, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            _asUtc = DateTime.MinValue;

            var isEmpty = string.IsNullOrWhiteSpace(value);
            if (isEmpty)
            {
                Parameters.Remove("TZID");
                _tzId = null;
                Value = DateTime.SpecifyKind(Value, DateTimeKind.Local);
                return;
            }

            var kind = string.Equals(value, "UTC", StringComparison.OrdinalIgnoreCase)
                ? DateTimeKind.Utc
                : DateTimeKind.Local;

            Value = DateTime.SpecifyKind(Value, kind);
            Parameters.Set("TZID", value);
            _tzId = value;
        }
    }

    /// <inheritdoc/>
    public string TimeZoneName => TzId;

    /// <inheritdoc/>
    public int Year => Value.Year;

    /// <inheritdoc/>
    public int Month => Value.Month;

    /// <inheritdoc/>
    public int Day => Value.Day;

    /// <inheritdoc/>
    public int Hour => Value.Hour;

    /// <inheritdoc/>
    public int Minute => Value.Minute;

    /// <inheritdoc/>
    public int Second => Value.Second;

    /// <inheritdoc/>
    public int Millisecond => Value.Millisecond;

    /// <inheritdoc/>
    public long Ticks => Value.Ticks;

    /// <inheritdoc/>
    public DayOfWeek DayOfWeek => Value.DayOfWeek;

    /// <inheritdoc/>
    public int DayOfYear => Value.DayOfYear;

    /// <inheritdoc/>
    public DateTime Date => Value.Date;

    /// <inheritdoc/>
    public TimeSpan TimeOfDay => Value.TimeOfDay;

    /// <inheritdoc/>
    public IDateTime ToTimeZone(string tzId)
    {
        if (string.IsNullOrWhiteSpace(tzId))
        {
            throw new ArgumentException("You must provide a valid time zone id", nameof(tzId));
        }

        // If TzId is empty, it's a system-local datetime, so we should use the system time zone as the starting point.
        var originalTzId = string.IsNullOrWhiteSpace(TzId)
            ? TimeZoneInfo.Local.Id
            : TzId;

        var zonedOriginal = DateUtil.ToZonedDateTimeLeniently(Value, originalTzId);
        var converted = zonedOriginal.WithZone(DateUtil.GetZone(tzId));

        return converted.Zone == DateTimeZone.Utc
            ? new vCardDateTime(converted.ToDateTimeUtc(), tzId)
            : new vCardDateTime(DateTime.SpecifyKind(converted.ToDateTimeUnspecified(), DateTimeKind.Local), tzId);
    }

    /// <inheritdoc/>
    public DateTimeOffset AsDateTimeOffset => string.IsNullOrWhiteSpace(TzId) ? new DateTimeOffset(AsSystemLocal) : DateUtil.ToZonedDateTimeLeniently(Value, TzId).ToDateTimeOffset();

    /// <inheritdoc/>
    public IDateTime Add(TimeSpan ts) => this + ts;

    /// <inheritdoc/>
    public IDateTime Subtract(TimeSpan ts) => this - ts;

    /// <inheritdoc/>
    public TimeSpan Subtract(IDateTime dt) => this - dt;

    /// <inheritdoc/>
    public IDateTime AddYears(int years)
    {
        var dt = Copy<IDateTime>();
        dt.Value = Value.AddYears(years);
        return dt;
    }

    /// <inheritdoc/>
    public IDateTime AddMonths(int months)
    {
        var dt = Copy<IDateTime>();
        dt.Value = Value.AddMonths(months);
        return dt;
    }

    /// <inheritdoc/>
    public IDateTime AddDays(int days)
    {
        var dt = Copy<IDateTime>();
        dt.Value = Value.AddDays(days);
        return dt;
    }

    /// <inheritdoc/>
    public IDateTime AddHours(int hours)
    {
        var dt = Copy<IDateTime>();
        if (!dt.HasTime && hours % 24 > 0)
        {
            dt.HasTime = true;
        }
        dt.Value = Value.AddHours(hours);
        return dt;
    }

    /// <inheritdoc/>
    public IDateTime AddMinutes(int minutes)
    {
        var dt = Copy<IDateTime>();
        if (!dt.HasTime && minutes % 1440 > 0)
        {
            dt.HasTime = true;
        }
        dt.Value = Value.AddMinutes(minutes);
        return dt;
    }

    /// <inheritdoc/>
    public IDateTime AddSeconds(int seconds)
    {
        var dt = Copy<IDateTime>();
        if (!dt.HasTime && seconds % 86400 > 0)
        {
            dt.HasTime = true;
        }
        dt.Value = Value.AddSeconds(seconds);
        return dt;
    }

    /// <inheritdoc/>
    public IDateTime AddMilliseconds(int milliseconds)
    {
        var dt = Copy<IDateTime>();
        if (!dt.HasTime && milliseconds % 86400000 > 0)
        {
            dt.HasTime = true;
        }
        dt.Value = Value.AddMilliseconds(milliseconds);
        return dt;
    }

    /// <inheritdoc/>
    public IDateTime AddTicks(long ticks)
    {
        var dt = Copy<IDateTime>();
        dt.HasTime = true;
        dt.Value = Value.AddTicks(ticks);
        return dt;
    }

    /// <inheritdoc/>
    public bool LessThan(IDateTime dt) => this < dt;

    /// <inheritdoc/>
    public bool GreaterThan(IDateTime dt) => this > dt;

    /// <inheritdoc/>
    public bool LessThanOrEqual(IDateTime dt) => this <= dt;

    /// <inheritdoc/>
    public bool GreaterThanOrEqual(IDateTime dt) => this >= dt;

    /// <inheritdoc/>
    public void AssociateWith(IDateTime dt)
    {
        if (AssociatedObject == null && dt.AssociatedObject != null)
        {
            AssociatedObject = dt.AssociatedObject;
        }
        else if (AssociatedObject != null && dt.AssociatedObject == null)
        {
            dt.AssociatedObject = AssociatedObject;
        }
    }

    /// <inheritdoc/>
    public int CompareTo(IDateTime dt)
    {
        if (Equals(dt))
        {
            return 0;
        }
        if (this < dt)
        {
            return -1;
        }
        if (this > dt)
        {
            return 1;
        }
        throw new Exception("An error occurred while comparing two IDateTime values.");
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return ToString(null, null);
    }

    /// <summary>
    /// Formats the value of the current instance using the specified format.
    /// </summary>
    /// <param name="format">The format to use. A null reference (Nothing in Visual Basic) to use the default format defined for the type of the System.IFormattable implementation.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    public string ToString(string format)
    {
        return ToString(format, null);
    }

    /// <inheritdoc/>
    public string ToString(string format, IFormatProvider formatProvider)
    {
        var tz = TimeZoneName;
        if (!string.IsNullOrEmpty(tz))
        {
            tz = " " + tz;
        }

        if (format != null)
        {
            return Value.ToString(format, formatProvider) + tz;
        }
        if (HasTime && HasDate)
        {
            return Value + tz;
        }
        if (HasTime)
        {
            return Value.TimeOfDay + tz;
        }
        return Value.ToString("d") + tz;
    }
}