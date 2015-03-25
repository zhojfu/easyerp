namespace Domain.Model.Factory
{
    using Infrastructure.Domain.Model;
    using System;

    public class MaterialStatisitc : BaseEntity, IAggregateRoot
    {
        public DateTime Date { get; set; }

        public double ComsumeQuantity { get; set; }

        public Guid MaterialId { get; set; }

        public virtual Product Material { get; set; } //material as same as the product
    }
}