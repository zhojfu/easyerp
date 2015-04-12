
namespace Doamin.Service.Factory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Model.Factory;
    using Infrastructure.Domain;
    using Infrastructure.Utility;

    public class EmployeeTimesheetService : IEmployeeTimesheetService
    {
        private readonly IRepository<WorkTimeStatistic> repository;

        private readonly IUnitOfWork unitOfWork;

        public EmployeeTimesheetService(IRepository<WorkTimeStatistic> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<WorkTimeStatistic> GetEmployeeTimesheetByDate(int employeeId, DateTime date)
        {
            var dateRange = DateHelper.GetWeekRangeOfCurrentDate(date);

            return this.repository.FindAll(
                m => m.EmployeeId == employeeId && m.Date >= dateRange.Item1 && m.Date <= dateRange.Item2);
        }

        public void UpdateTimesheet(int employeeId, Dictionary<DateTime, double> worktimes)
        {
            const double Tolerance = 0.001;

            foreach (var worktime in worktimes)
            {
                double hour = worktime.Value;
                DateTime date = worktime.Key;

                if (Math.Abs(hour) < Tolerance)
                {
                    continue;
                }

                WorkTimeStatistic wt = this.repository.FindAll(m => m.EmployeeId == employeeId && m.Date == date).FirstOrDefault();
                if (wt != null && Math.Abs(hour - wt.WorkTimeHr) > Tolerance)
                {
                    wt.WorkTimeHr = hour;
                    this.repository.Update(wt);
                }
                else
                {
                    WorkTimeStatistic w = new WorkTimeStatistic
                    {
                        EmployeeId = employeeId,
                        WorkTimeHr = hour,
                        Date = date,
                    };

                    this.repository.Add(w);
                }
            }
           
            this.unitOfWork.Commit();
        }
    }
}
