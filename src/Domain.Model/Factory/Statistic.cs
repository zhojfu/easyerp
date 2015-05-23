namespace Domain.Model.Factory
{
    using System;
    using Infrastructure.Domain.Model;

    public class Statistic : BaseEntity
    {
        public DateTime Date { get; set; }

        public double Value { get; set; }
    }
}