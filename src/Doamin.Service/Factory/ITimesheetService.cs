using System;
using System.Collections.Generic;

namespace Doamin.Service.Factory
{
    using Domain.Model.Factory;

    public interface ITimesheetService
    {
        IEnumerable<Timesheet> GetTimesheetByDate(int page, int pageSize, DateTime date);
        void UpdateTimesheet(DateTime dayOfWeek, Timesheet timesheet);
    }
}
