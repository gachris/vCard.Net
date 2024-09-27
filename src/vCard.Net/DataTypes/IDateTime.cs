namespace vCard.Net.DataTypes;

/// <summary>
/// Represents a date and time value with support for various operations and conversions.
/// </summary>
public interface IDateTime : IEncodableDataType, IComparable<IDateTime>, IFormattable, IvCardDataType
{
    /// <summary>
    /// Converts the date/time to the local date/time of this computer's time zone.
    /// </summary>
    DateTime AsSystemLocal { get; }

    /// <summary>
    /// Converts the date/time to UTC (Coordinated Universal Time).
    /// </summary>
    DateTime AsUtc { get; }

    /// <summary>
    /// Returns a DateTimeOffset representation of the value. If a time zone ID (TzId) is specified, it will use that time zone's UTC offset; otherwise, it will use the system-local time zone.
    /// </summary>
    DateTimeOffset AsDateTimeOffset { get; }

    /// <summary>
    /// Gets or sets a value indicating whether the value of this date/time represents a universal time.
    /// </summary>
    bool IsUtc { get; }

    /// <summary>
    /// Gets the time zone name associated with this date/time, if applicable.
    /// </summary>
    string TimeZoneName { get; }

    /// <summary>
    /// Gets or sets the underlying DateTime value stored. This should always use DateTimeKind.Utc, regardless of its actual representation. Use IsUtc along with the TzId to control how this date/time is handled.
    /// </summary>
    DateTime Value { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not this date/time value contains a 'date' part.
    /// </summary>
    bool HasDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not this date/time value contains a 'time' part.
    /// </summary>
    bool HasTime { get; set; }

    /// <summary>
    /// Gets or sets the time zone ID for this date/time value.
    /// </summary>
    string TzId { get; set; }

    /// <summary>
    /// Gets the year component of this date/time value.
    /// </summary>
    int Year { get; }

    /// <summary>
    /// Gets the month component of this date/time value.
    /// </summary>
    int Month { get; }

    /// <summary>
    /// Gets the day component of this date/time value.
    /// </summary>
    int Day { get; }

    /// <summary>
    /// Gets the hour component of this date/time value.
    /// </summary>
    int Hour { get; }

    /// <summary>
    /// Gets the minute component of this date/time value.
    /// </summary>
    int Minute { get; }

    /// <summary>
    /// Gets the second component of this date/time value.
    /// </summary>
    int Second { get; }

    /// <summary>
    /// Gets the millisecond component of this date/time value.
    /// </summary>
    int Millisecond { get; }

    /// <summary>
    /// Gets the ticks component of this date/time value.
    /// </summary>
    long Ticks { get; }

    /// <summary>
    /// Gets the day of the year represented by this date/time value.
    /// </summary>
    int DayOfYear { get; }

    /// <summary>
    /// Gets the day of the week represented by this date/time value.
    /// </summary>
    DayOfWeek DayOfWeek { get; }

    /// <summary>
    /// Gets the time of day component of this date/time value.
    /// </summary>
    TimeSpan TimeOfDay { get; }

    /// <summary>
    /// Gets the date portion of this date/time value.
    /// </summary>
    DateTime Date { get; }

    /// <summary>
    /// Converts the date/time value to a local time within the specified time zone.
    /// </summary>
    /// <param name="tzId">The time zone ID.</param>
    /// <returns>A new instance of <see cref="IDateTime"/> representing the local time within the specified time zone.</returns>
    IDateTime ToTimeZone(string tzId);

    /// <summary>
    /// Adds the specified time interval to this date/time value.
    /// </summary>
    /// <param name="ts">The time interval to add.</param>
    /// <returns>A new instance of <see cref="IDateTime"/> representing the result of the addition.</returns>
    IDateTime Add(TimeSpan ts);

    /// <summary>
    /// Subtracts the specified time interval from this date/time value.
    /// </summary>
    /// <param name="ts">The time interval to subtract.</param>
    /// <returns>A new instance of <see cref="IDateTime"/> representing the result of the subtraction.</returns>
    IDateTime Subtract(TimeSpan ts);

    /// <summary>
    /// Subtracts the specified date/time value from this date/time value, returning the time interval between them.
    /// </summary>
    /// <param name="dt">The date/time value to subtract.</param>
    /// <returns>The time interval between this date/time value and the specified date/time value.</returns>
    TimeSpan Subtract(IDateTime dt);

    /// <summary>
    /// Adds the specified number of years to the current date/time value.
    /// </summary>
    /// <param name="years">The number of years to add.</param>
    /// <returns>A new instance of <see cref="IDateTime"/> representing the result of the addition.</returns>
    IDateTime AddYears(int years);

    /// <summary>
    /// Adds the specified number of months to the current date/time value.
    /// </summary>
    /// <param name="months">The number of months to add.</param>
    /// <returns>A new instance of <see cref="IDateTime"/> representing the result of the addition.</returns>
    IDateTime AddMonths(int months);

    /// <summary>
    /// Adds the specified number of days to the current date/time value.
    /// </summary>
    /// <param name="days">The number of days to add.</param>
    /// <returns>A new instance of <see cref="IDateTime"/> representing the result of the addition.</returns>
    IDateTime AddDays(int days);

    /// <summary>
    /// Adds the specified number of hours to the current date/time value.
    /// </summary>
    /// <param name="hours">The number of hours to add.</param>
    /// <returns>A new instance of <see cref="IDateTime"/> representing the result of the addition.</returns>
    IDateTime AddHours(int hours);

    /// <summary>
    /// Adds the specified number of minutes to the current date/time value.
    /// </summary>
    /// <param name="minutes">The number of minutes to add.</param>
    /// <returns>A new instance of <see cref="IDateTime"/> representing the result of the addition.</returns>
    IDateTime AddMinutes(int minutes);

    /// <summary>
    /// Adds the specified number of seconds to the current date/time value.
    /// </summary>
    /// <param name="seconds">The number of seconds to add.</param>
    /// <returns>A new instance of <see cref="IDateTime"/> representing the result of the addition.</returns>
    IDateTime AddSeconds(int seconds);

    /// <summary>
    /// Adds the specified number of milliseconds to the current date/time value.
    /// </summary>
    /// <param name="milliseconds">The number of milliseconds to add.</param>
    /// <returns>A new instance of <see cref="IDateTime"/> representing the result of the addition.</returns>
    IDateTime AddMilliseconds(int milliseconds);

    /// <summary>
    /// Adds the specified number of ticks to the current date/time value.
    /// </summary>
    /// <param name="ticks">The number of ticks to add.</param>
    /// <returns>A new instance of <see cref="IDateTime"/> representing the result of the addition.</returns>
    IDateTime AddTicks(long ticks);

    /// <summary>
    /// Determines whether the current date/time value is less than the specified date/time value.
    /// </summary>
    /// <param name="dt">The date/time value to compare with.</param>
    /// <returns>True if the current date/time value is less than the specified date/time value; otherwise, false.</returns>
    bool LessThan(IDateTime dt);

    /// <summary>
    /// Determines whether the current date/time value is greater than the specified date/time value.
    /// </summary>
    /// <param name="dt">The date/time value to compare with.</param>
    /// <returns>True if the current date/time value is greater than the specified date/time value; otherwise, false.</returns>
    bool GreaterThan(IDateTime dt);

    /// <summary>
    /// Determines whether the current date/time value is less than or equal to the specified date/time value.
    /// </summary>
    /// <param name="dt">The date/time value to compare with.</param>
    /// <returns>True if the current date/time value is less than or equal to the specified date/time value; otherwise, false.</returns>
    bool LessThanOrEqual(IDateTime dt);

    /// <summary>
    /// Determines whether the current date/time value is greater than or equal to the specified date/time value.
    /// </summary>
    /// <param name="dt">The date/time value to compare with.</param>
    /// <returns>True if the current date/time value is greater than or equal to the specified date/time value; otherwise, false.</returns>
    bool GreaterThanOrEqual(IDateTime dt);

    /// <summary>
    /// Associates this date/time value with another date/time value.
    /// </summary>
    /// <param name="dt">The date/time value to associate with.</param>
    void AssociateWith(IDateTime dt);
}