namespace Domain.EntityFramework.Configurations
{
    using Domain.Model;
    using System.Data.Entity.ModelConfiguration;

    public class ProductCategoryConfiguration : EntityTypeConfiguration<ProductCategory>
    {
        public ProductCategoryConfiguration()
        {
            {
                this.ToTable("Product_Category_Mapping");
                this.HasKey(pc => pc.Id);

                this.HasRequired(pc => pc.Category)
                    .WithMany()
                    .HasForeignKey(pc => pc.CategoryId);

                this.HasRequired(pc => pc.Product)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(pc => pc.ProductId);
            }
        }
    }
}