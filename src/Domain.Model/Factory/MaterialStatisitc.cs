namespace Domain.Model.Factory
{
    using Domain.Model.Products;
    using Infrastructure.Domain.Model;

    public class MaterialStatisitc : Statistic, IAggregateRoot
    {
        public double ComsumeQuantity { get; set; }

        public long MaterialId { get; set; }

        public virtual Product Material { get; set; }
    }
}