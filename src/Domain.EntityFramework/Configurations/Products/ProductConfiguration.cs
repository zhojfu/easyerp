namespace Domain.EntityFramework.Configurations.Products
{
    using Domain.Model.Products;
    using System.Data.Entity.ModelConfiguration;

    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            this.ToTable("Product");
            this.HasKey(p => p.Id);
            this.Property(p => p.Name).IsRequired().HasMaxLength(400);
            this.Property(p => p.Sku).HasMaxLength(400);
            this.Property(p => p.Gtin).HasMaxLength(400);
            this.Property(p => p.Price).HasPrecision(18, 4);
            this.Property(p => p.ProductCost).HasPrecision(18, 4);
            this.Property(p => p.Weight).HasPrecision(18, 4);
            this.Property(p => p.Length).HasPrecision(18, 4);
            this.Property(p => p.Width).HasPrecision(18, 4);
            this.Property(p => p.Height).HasPrecision(18, 4);
            this.HasRequired(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId);
        }
    }
}