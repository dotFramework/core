namespace System
{
    public static class DateTimeExtensions
    {
        public static DateTime ConvertTime(this DateTime dateTime, string timeZone)
        {
            if (String.IsNullOrEmpty(timeZone))
            {
                throw new ArgumentNullException("TimeZone");
            }

            if (dateTime.Kind != DateTimeKind.Utc)
            {
                throw new InvalidTimeZoneException();
            }

            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.FindSystemTimeZoneById(timeZone));
        }

        public static DateTimeOffset ConvertTime(this DateTimeOffset dateTimeOffset, string timeZone)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTimeOffset, timeZone);
        }
    }
}
