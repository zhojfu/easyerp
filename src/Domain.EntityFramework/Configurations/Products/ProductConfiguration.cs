using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

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
            Property(p => p.Price).HasPrecision(18, 4).IsOptional();
            Property(p => p.ProductCost).HasPrecision(18, 4).IsOptional();
            Property(p => p.Weight).HasPrecision(18, 4).IsOptional();
            Property(p => p.Length).HasPrecision(18, 4).IsOptional();
            Property(p => p.Width).HasPrecision(18, 4).IsOptional();
            Property(p => p.Height).HasPrecision(18, 4).IsOptional();
            HasRequired(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId);
            Property(t => t.ItemNo).IsRequired().HasMaxLength(6).HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("IX_ItemNo", 1) {IsUnique = true}));
        }
    }
}