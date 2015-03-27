
namespace Domain.Model.Factory
{
    using System;
    using Infrastructure.Domain.Model;

    public class WorkTimeStatistic : Statistic, IAggregateRoot
    {
        public double SalaryOfDay { get; set; }

        public double WorkTimeHr { get; set; }

        public Guid EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
