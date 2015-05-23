namespace Doamin.Service.Factory
{
    using System;
    using System.Collections.Generic;
    using Domain.Model.Factory;

    public interface ITimesheetService<T>
    {
        IEnumerable<Timesheet> GetTimesheetByDate(int page, int pageSize, DateTime date);
        void UpdateTimesheet(DateTime dayOfWeek, Timesheet timesheet);
    }
}