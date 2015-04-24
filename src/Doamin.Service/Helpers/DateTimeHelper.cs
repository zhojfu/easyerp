namespace Doamin.Service.Helpers
{
    using System;
    using System.Collections.ObjectModel;
    using Domain.Model.Users;

    /// <summary>
    /// Represents a datetime helper
    /// </summary>
    public class DateTimeHelper : IDateTimeHelper
    {
        public TimeZoneInfo FindTimeZoneById(string id)
        {
            throw new NotImplementedException();
        }

        public ReadOnlyCollection<TimeZoneInfo> GetSystemTimeZones()
        {
            throw new NotImplementedException();
        }

        public DateTime ConvertToUserTime(DateTime dt)
        {
            return ConvertToUserTime(dt, dt.Kind);
        }

        public DateTime ConvertToUserTime(DateTime dt, DateTimeKind sourceDateTimeKind)
        {
            dt = DateTime.SpecifyKind(dt, sourceDateTimeKind);
            var currentUserTimeZoneInfo = CurrentTimeZone;
            return TimeZoneInfo.ConvertTime(dt, currentUserTimeZoneInfo);
        }

        public DateTime ConvertToUserTime(DateTime dt, TimeZoneInfo sourceTimeZone)
        {
            var currentUserTimeZoneInfo = CurrentTimeZone;
            return ConvertToUserTime(dt, sourceTimeZone, currentUserTimeZoneInfo);
        }

        public DateTime ConvertToUserTime(DateTime dt, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone)
        {
            return TimeZoneInfo.ConvertTime(dt, sourceTimeZone, destinationTimeZone);
        }

        public DateTime ConvertToUtcTime(DateTime dt)
        {
            return ConvertToUtcTime(dt, dt.Kind);
        }

        public DateTime ConvertToUtcTime(DateTime dt, DateTimeKind sourceDateTimeKind)
        {
            dt = DateTime.SpecifyKind(dt, sourceDateTimeKind);
            return TimeZoneInfo.ConvertTimeToUtc(dt);
        }

        public DateTime ConvertToUtcTime(DateTime dt, TimeZoneInfo sourceTimeZone)
        {
            if (sourceTimeZone.IsInvalidTime(dt))
            {
                //could not convert
                return dt;
            }

            return TimeZoneInfo.ConvertTimeToUtc(dt, sourceTimeZone);
        }

        public TimeZoneInfo GetCustomerTimeZone(User user)
        {
            throw new NotImplementedException();
        }

        public TimeZoneInfo DefaultStoreTimeZone
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public TimeZoneInfo CurrentTimeZone
        {
            get { return TimeZoneInfo.Local; }
            set { throw new NotImplementedException(); }
        }
    }
}