
namespace Domain.Model.Factory
{
    using System.Collections.Generic;
    using Infrastructure.Domain.Model;

    /*water, power...*/
    public class Consumption : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public override string DisplayName
        {
            get { return this.Name; }
        }

        public string Unit { get; set; }

        public decimal PriceOfUnit { get; set; }

        public virtual ICollection<ConsumptionStatistic> ConsumptionRecords { get; set; }
    }
}
