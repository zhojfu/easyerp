namespace Domain.Model.Factory
{
    using Infrastructure.Domain.Model;
    using System;

    public class AttritionStatistic : Statistic, IAggregateRoot
    {
        public double Volume { get; set; }

        public double PriceOfUnit { get; set; }

        public long AttritionId { get; set; }

        public virtual Attrition Attrition { get; set; }
    }
}