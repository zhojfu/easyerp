
namespace Doamin.Service.Factory
{
    using System;
    using System.Collections.Generic;
    using Domain.Model.Factory;
    using Infrastructure.Domain;
    using Infrastructure.Utility;

    public class EmployeeTimesheetService
    {
        private readonly IRepository<WorkTimeStatistic> repository;

        private readonly IUnitOfWork unitOfWork;

        public EmployeeTimesheetService(IRepository<WorkTimeStatistic> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<WorkTimeStatistic> GetEmployeeTimesheetByDate(Guid employeeId, DateTime date)
        {
            var dateRange = DateHelper.GetWeekRangeOfCurrentDate(date);

            return this.repository.FindAll(
                m => m.EmployeeId == employeeId && m.Date >= dateRange.Item1 && m.Date <= dateRange.Item2);
        }
    }
}
