

namespace Doamin.Service.Factory
{
    using System;

    public static class DateUtils
    {
        private const int WeekDays = 7;

        public static Tuple<DateTime, DateTime> GetWeekRangeOfCurrentDate(DateTime now)
        {
            int currentDay = (int)now.DayOfWeek;

            double firstDayOfWeek = -((currentDay + WeekDays) - 1) % WeekDays;

            double lastDayOfWeek = (WeekDays - currentDay) % WeekDays;
           
            return new Tuple<DateTime, DateTime>(now.AddDays(firstDayOfWeek), now.AddDays(lastDayOfWeek));
        }
    }
}
