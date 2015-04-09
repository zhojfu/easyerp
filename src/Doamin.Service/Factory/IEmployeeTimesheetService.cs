
namespace Doamin.Service.Factory
{
    using System;
    using System.Collections.Generic;
    using Domain.Model.Factory;

    public interface IEmployeeTimesheetService
    {
        IEnumerable<WorkTimeStatistic> GetEmployeeTimesheetByDate(Guid employeeId, DateTime from);
    }
}
