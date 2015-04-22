namespace Doamin.Service.Factory
{
    using System;
    using System.Collections.Generic;
    using Domain.Model.Factory;

    public interface IEmployeeTimesheetService
    {
        IEnumerable<WorkTimeStatistic> GetEmployeeTimesheetByDate(int employeeId, DateTime from);
        void UpdateTimesheet(int employeeId, Dictionary<DateTime, double> worktimes);
    }
}