namespace Domain.Model.Factory
{
    using System;
    using Infrastructure.Domain.Model;

    public class AttritionStatistic : Statistic, IAggregateRoot
    {
        public double Volume { get; set; }

        public double PriceOfUnit { get; set; }

        public Guid AttritionId { get; set; }

        public virtual Attrition Attrition { get; set; }
    }
}
