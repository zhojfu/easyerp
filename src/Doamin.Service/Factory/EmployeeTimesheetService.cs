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
        private readonly IEmployeeService employeeService;

        private readonly IRepository<WorkTimeStatistic> repository;

        public EmployeeTimesheetService(
            IRepository<WorkTimeStatistic> repository,
            IUnitOfWork unitOfWork,
            IEmployeeService employeeService)
            : base(unitOfWork)
        {
            this.repository = repository;
            this.employeeService = employeeService;
        }

        public IEnumerable<WorkTimeStatistic> GetEmployeeTimesheetByDate(int employeeId, DateTime date)
        {
            var dateRange = DateHelper.GetWeekRangeOfCurrentDate(date);

            return repository.FindAll(
                m => m.EmployeeId == employeeId && m.Date >= dateRange.Item1 && m.Date <= dateRange.Item2);
        }

        protected override PagedResult<Employee> GetCategories(int page, int pageSize)
        {
            return employeeService.GetEmployees(page, pageSize);
        }

        protected override IEnumerable<WorkTimeStatistic> GetTimesheetOfWeekByCategory(
            int categoryId,
            DateTime dateOfWeek)
        {
            var dateRange = DateHelper.GetWeekRangeOfCurrentDate(dateOfWeek);

            return repository.FindAll(
                m => m.EmployeeId == categoryId && m.Date >= dateRange.Item1 && m.Date <= dateRange.Item2);
        }

        protected override WorkTimeStatistic FindSpecificDataOfDateTime(int categoryId, DateTime date)
        {
            return repository.FindAll(m => m.EmployeeId == categoryId && m.Date == date).FirstOrDefault();
        }

        protected override void UpdateDataOfTime(WorkTimeStatistic s)
        {
            repository.Update(s);
        }

        protected override void AddNewDataForTime(int categoryId, double value, DateTime date)
        {
            var c = new WorkTimeStatistic
            {
                EmployeeId = categoryId,
                Value = value,
                Date = date
            };

            repository.Add(c);
        }
    }
}