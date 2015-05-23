namespace Domain.Model.Products
{
    using Infrastructure.Domain.Model;

    public class ProductManufacturer : BaseEntity
    {
        public int ProductId { get; set; }

        public int ManufacturerId { get; set; }

        public bool IsFeaturedProduct { get; set; }

        public int DisplayOrder { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual Product Product { get; set; }
    }
}