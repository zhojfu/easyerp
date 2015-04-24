namespace EasyERP.Web.Framework
{
    using Doamin.Service.Helpers;
    using EasyErp.Core.Infrastructure;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    /// <summary>
    /// Extensions
    /// </summary>
    public static class Extensions
    {
        public static SelectList ToSelectList<TEnum>(
            this TEnum enumObj,
            bool markCurrentAsSelected = true,
            int[] valuesToExclude = null) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("An Enumeration type is required.", "enumObj");
            }

            var values = from TEnum enumValue in Enum.GetValues(typeof(TEnum))
                         where valuesToExclude == null || !valuesToExclude.Contains(Convert.ToInt32(enumValue))
                         select new
                         {
                             ID = Convert.ToInt32(enumValue),
                             Name = enumValue.ToString()
                         };
            object selectedValue = null;
            if (markCurrentAsSelected)
            {
                selectedValue = Convert.ToInt32(enumObj);
            }
            return new SelectList(values, "ID", "Name", selectedValue);
        }

        public static string RelativeFormat(this DateTime source)
        {
            return RelativeFormat(source, string.Empty);
        }

        public static string RelativeFormat(this DateTime source, string defaultFormat)
        {
            return RelativeFormat(source, false, defaultFormat);
        }

        public static string RelativeFormat(
            this DateTime source,
            bool convertToUserTime,
            string defaultFormat)
        {
            var result = "";

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - source.Ticks);
            var delta = ts.TotalSeconds;

            if (delta > 0)
            {
                if (delta < 60) // 60 (seconds)
                {
                    result = ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
                }
                else if (delta < 120) //2 (minutes) * 60 (seconds)
                {
                    result = "a minute ago";
                }
                else if (delta < 2700) // 45 (minutes) * 60 (seconds)
                {
                    result = ts.Minutes + " minutes ago";
                }
                else if (delta < 5400) // 90 (minutes) * 60 (seconds)
                {
                    result = "an hour ago";
                }
                else if (delta < 86400) // 24 (hours) * 60 (minutes) * 60 (seconds)
                {
                    var hours = ts.Hours;
                    if (hours == 1)
                    {
                        hours = 2;
                    }
                    result = hours + " hours ago";
                }
                else if (delta < 172800) // 48 (hours) * 60 (minutes) * 60 (seconds)
                {
                    result = "yesterday";
                }
                else if (delta < 2592000) // 30 (days) * 24 (hours) * 60 (minutes) * 60 (seconds)
                {
                    result = ts.Days + " days ago";
                }
                else if (delta < 31104000) // 12 (months) * 30 (days) * 24 (hours) * 60 (minutes) * 60 (seconds)
                {
                    var months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                    result = months <= 1 ? "one month ago" : months + " months ago";
                }
                else
                {
                    var years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                    result = years <= 1 ? "one year ago" : years + " years ago";
                }
            }
            else
            {
                var tmp1 = source;
                if (convertToUserTime)
                {
                    tmp1 = EngineContext.Current.Resolve<IDateTimeHelper>().ConvertToUserTime(tmp1, DateTimeKind.Utc);
                }

                //default formatting
                if (!String.IsNullOrEmpty(defaultFormat))
                {
                    result = tmp1.ToString(defaultFormat);
                }
                else
                {
                    result = tmp1.ToString();
                }
            }
            return result;
        }
    }
}