namespace Domain.Model.Factory
{
    using System;
    using Infrastructure.Domain.Model;

    public class MaterialStatisitc : BaseEntity, IAggregateRoot
    {
        public DateTime Date { get; set; }

        public double ComsumeQuantity { get; set; }

        public string MaterialId { get; set; }

        public virtual Product Material { get; set; } //material as same as the product
    }
}
