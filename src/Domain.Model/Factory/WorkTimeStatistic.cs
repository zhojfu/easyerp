namespace Domain.Model.Factory
{
    using System;
    using Infrastructure.Domain.Model;

    public class WorkTimeStatistic: BaseEntity, IAggregateRoot
    {
        public double SalaryOfDay { get; set; }
        public double WorkTimeHr { get; set; }
        public DateTime Date { get; set; }

        public Guid WorkerId { get; set; }
        public virtual Worker Worker { get; set; }
    }
}
