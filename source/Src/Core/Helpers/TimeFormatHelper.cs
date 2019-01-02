using System;
using System.Globalization;

namespace DotFramework.Core
{
    public static class TimeFormatHelper
    {
        private static readonly CultureInfo _CultureInfo = CultureInfo.CurrentCulture;

        public static string GetAmPmOnly(DateTime dt)
        {
            if (!_CultureInfo.DateTimeFormat.LongTimePattern.Contains("tt"))
            {
                return "";
            }

            return dt.ToString("tt");
        }

        private static string _HoursMinutesSplitter;
        public static string HoursMinutesSplitter
        {
            get
            {
                if (_HoursMinutesSplitter == null)
                {
                    if (TimePattern.Contains("."))
                    {
                        _HoursMinutesSplitter = ".";
                    }
                    else
                    {
                        _HoursMinutesSplitter = ":";
                    }
                }

                return _HoursMinutesSplitter;
            }
        }

        private static string _HoursPattern;
        public static string HoursPattern
        {
            get
            {
                if (_HoursPattern == null)
                {
                    _HoursPattern = _CultureInfo.DateTimeFormat.ShortTimePattern.Replace(":mm", "").Replace(".mm", "").Replace(":ss", "").Replace(".ss", "").Replace(" tt", "").Replace("tt ", "").Replace("' h '", "").Replace("mm", "");
                    if (_HoursPattern.Length == 1)
                    {
                        _HoursPattern = _HoursPattern.Insert(0, "%");
                    }
                }
                return _HoursPattern;
            }
        }

        private static bool? _Is24Format;
        public static bool Is24Format
        {
            get
            {
                if (!_Is24Format.HasValue)
                {
                    bool? nullable = _Is24Format = new bool?(!_CultureInfo.DateTimeFormat.LongTimePattern.Contains("tt"));
                    return nullable.Value;
                }
                return _Is24Format.Value;
            }
        }

        private static string _TimePattern;
        public static string TimePattern
        {
            get
            {
                if (_TimePattern == null)
                {
                    _TimePattern = _CultureInfo.DateTimeFormat.LongTimePattern.Replace(":ss", "").Replace(".ss", "");
                }

                return _TimePattern;
            }
        }
    }
}
