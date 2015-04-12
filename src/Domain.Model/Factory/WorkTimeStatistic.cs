namespace Domain.Model.Factory
{
    using Infrastructure.Domain.Model;
    using System;

    public class WorkTimeStatistic : Statistic, IAggregateRoot
    {
        public double SalaryOfDay { get; set; }

        public double WorkTimeHr { get; set; }

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}