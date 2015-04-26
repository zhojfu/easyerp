namespace Infrastructure.Utility
{
    using System;

    public static class DateHelper
    {
        private const int WeekDays = 7;

        public static Tuple<DateTime, DateTime> GetWeekRangeOfCurrentDate(DateTime now)
        {
            var currentDay = (int)now.DayOfWeek;

            double firstDayOfWeek = -((currentDay + WeekDays) - 1) % WeekDays;

            double lastDayOfWeek = (WeekDays - currentDay) % WeekDays;

            return new Tuple<DateTime, DateTime>(now.AddDays(firstDayOfWeek), now.AddDays(lastDayOfWeek));
        }
    }
}