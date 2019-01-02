namespace System
{
    public static class EpochTimeExtensions
    {
        /// <summary>
        /// Converts the given date value to epoch time.
        /// </summary>
        public static long ToEpochTime(this DateTime dateTime, string timeZone = null)
        {
            var time = dateTime.ToUniversalTime();

            if (timeZone != null)
            {
                time = TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.FindSystemTimeZoneById(timeZone));
            }

            var ticks = time.Ticks - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).Ticks;
            var ts = ticks / TimeSpan.TicksPerSecond;
            return ts;
        }

        /// <summary>
        /// Converts the given epoch time to a <see cref="DateTime"/>
        /// </summary>
        public static DateTime ToDateTimeFromEpoch(this long longDate, string timeZone = null)
        {
            var timeInTicks = longDate * TimeSpan.TicksPerSecond;
            var time = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(timeInTicks);

            if (timeZone != null)
            {
                time = TimeZoneInfo.ConvertTime(time, TimeZoneInfo.FindSystemTimeZoneById(timeZone));
            }

            return time;
        }
    }
}
