namespace Domain.Model.Factory
{
    using Infrastructure.Domain.Model;
    using System;

    public class MaterialStatisitc : Statistic, IAggregateRoot
    {
        public double ComsumeQuantity { get; set; }

        public Guid MaterialId { get; set; }

        public virtual Product Material { get; set; } //material as same as the product
    }
}