namespace Domain.EntityFramework.Configurations
{
    using Domain.Model;
    using System.Data.Entity.ModelConfiguration;

    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            this.HasKey(t => t.Upc).ToTable("Products");
            this.Property(t => t.Upc).HasMaxLength(50);
            this.Property(t => t.Name).HasMaxLength(50);
            this.Property(t => t.Description).HasMaxLength(50);
            this.Property(t => t.Unit);
            this.HasMany(t => t.Prices);

            //this.HasMany(t => t.RepositoryStocks);
        }
    }
}