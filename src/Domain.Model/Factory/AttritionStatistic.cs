namespace Domain.Model.Factory
{
    using Infrastructure.Domain.Model;

    public class AttritionStatistic : Statistic, IAggregateRoot
    {
        public double Volume { get; set; }
        public double PriceOfUnit { get; set; }
        public int AttritionId { get; set; }
        public virtual Attrition Attrition { get; set; }
    }
}