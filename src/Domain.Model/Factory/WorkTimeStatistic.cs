
namespace Domain.Model.Factory
{
    using System;
    using Infrastructure.Domain.Model;

    public class WorkTimeStatistic : BaseEntity, IAggregateRoot
    {
        public double SalaryOfDay { get; set; }

        public double WorkTimeHr { get; set; }

        public DateTime Date { get; set; }

        public Guid EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
