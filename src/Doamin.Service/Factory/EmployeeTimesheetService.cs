
namespace Doamin.Service.Factory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Model;
    using Domain.Model.Factory;
    using Infrastructure.Domain;
    using Infrastructure.Utility;

    public class EmployeeTimesheetService : TimesheetService<Employee, WorkTimeStatistic>
    {
        private readonly IRepository<WorkTimeStatistic> repository;

        private readonly IEmployeeService employeeService;

        public EmployeeTimesheetService(IRepository<WorkTimeStatistic> repository, IUnitOfWork unitOfWork, IEmployeeService employeeService)
            : base(unitOfWork)
        {
            this.repository = repository;
            this.employeeService = employeeService;
        }

        public IEnumerable<WorkTimeStatistic> GetEmployeeTimesheetByDate(int employeeId, DateTime date)
        {
            var dateRange = DateHelper.GetWeekRangeOfCurrentDate(date);

            return this.repository.FindAll(
                m => m.EmployeeId == employeeId && m.Date >= dateRange.Item1 && m.Date <= dateRange.Item2);
        }

        protected override PagedResult<Employee> GetCategories(int page, int pageSize)
        {
            return this.employeeService.GetEmployees(page, pageSize);
        }

        protected override IEnumerable<WorkTimeStatistic> GetTimesheetOfWeekByCategory(int categoryId, DateTime dateOfWeek)
        {
            var dateRange = DateHelper.GetWeekRangeOfCurrentDate(dateOfWeek);

            return this.repository.FindAll(
                m => m.EmployeeId == categoryId && m.Date >= dateRange.Item1 && m.Date <= dateRange.Item2);
        }

        protected override WorkTimeStatistic FindSpecificDataOfDateTime(int categoryId, DateTime date)
        {
            return this.repository.FindAll(m => m.EmployeeId == categoryId && m.Date == date).FirstOrDefault();
        }

        protected override void UpdateDataOfTime(WorkTimeStatistic s)
        {
            this.repository.Update(s);
        }

       
        protected override void AddNewDataForTime(int categoryId, double value, DateTime date)
        {
            WorkTimeStatistic c = new WorkTimeStatistic
            {
                EmployeeId = categoryId,
                Value = value,
                Date = date,
            };

            this.repository.Add(c);
        }

        /*public void UpdateTimesheet(int employeeId, Dictionary<DateTime, double> worktimes)
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
                if (wt != null )
                {
                    if(Math.Abs(hour - wt.WorkTimeHr) < Tolerance)
                        continue;
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
        }*/
    }
}
