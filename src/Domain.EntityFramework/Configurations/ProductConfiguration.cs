using System.Data.Entity.ModelConfiguration;
using Domain.Model;

namespace Domain.EntityFramework.Configurations
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            HasKey(t => t.Id).ToTable("Products");
            Property(t => t.Upc).HasMaxLength(50);
            Property(t => t.Name).HasMaxLength(50);
            Property(t => t.Description).HasMaxLength(50);
            Property(t => t.Price);
            Property(t => t.Cost);
            Property(t => t.Volume);
            Property(t => t.Origin).HasMaxLength(50);
        }
    }
}
