namespace Domain.Model.Products
{
    using Infrastructure.Domain.Model;

    public class ProductAttribute : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}