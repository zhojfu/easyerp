namespace Domain.Model.Factory
{
    using System;
    using Infrastructure.Domain.Model;

    public class AttritionStatistic : BaseEntity, IAggregateRoot
    {
        public DateTime Date { get; set; }

        public double Volume { get; set; }

        public double PriceOfUnit { get; set; }

        public Guid AttritionId { get; set; }

        public virtual Attrition Attrition { get; set; }
    }
}
