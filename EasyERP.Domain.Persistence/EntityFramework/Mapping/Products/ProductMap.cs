using EasyERP.Domain.Model;

namespace EasyERP.Domain.Persistence.EntityFramework.Mapping.Products
{
    class ProductMap : NopEntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            this.ToTable("Product");
            this.HasKey(o => o.ProductId)
                .Property(o => o.Name);
        }
    }
}
