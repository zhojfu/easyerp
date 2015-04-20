namespace Domain.Model.Factory
{
    using Infrastructure.Domain.Model;

    public class ConsumptionStatistic : Statistic, IAggregateRoot
    {
        public double Volume { get; set; }

        public double PriceOfUnit { get; set; }

        public int ConsumptionId { get; set; }

        public virtual Consumption Consumption { get; set; }
    }
}