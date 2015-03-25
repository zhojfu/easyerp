namespace Domain.Model.Factory
{
    using System.Collections.Generic;
    using Infrastructure.Domain.Model;

    /*water, power...*/
    public class Attrition : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public string Unit { get; set; }

        public string Cost { get; set; }

        public virtual ICollection<AttritionStatistic> AttritionRecords { get; set; }
    }
}
