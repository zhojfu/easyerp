namespace Domain.EntityFramework.Configurations.Products
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Products;

    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            ToTable("Product");
            HasKey(p => p.Id);
            Property(p => p.Name).IsRequired().HasMaxLength(400);
            Property(p => p.Gtin).HasMaxLength(400);
            Property(p => p.Price).HasPrecision(18, 4);
            Property(p => p.ProductCost).HasPrecision(18, 4);
            Property(p => p.Weight).HasPrecision(18, 4);
            Property(p => p.Length).HasPrecision(18, 4);
            Property(p => p.Width).HasPrecision(18, 4);
            Property(p => p.Height).HasPrecision(18, 4);
            Property(p => p.ItemNo).IsRequired();
            HasRequired(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId);
            HasMany(p => p.Stores).WithMany(w=>w.Products).Map(m => m.ToTable("Project_Store_Mapping"));

        }
    }
}