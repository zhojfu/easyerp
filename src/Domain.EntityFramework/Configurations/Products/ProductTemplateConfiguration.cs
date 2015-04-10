namespace Domain.EntityFramework.Configurations.Products
{
    using Domain.Model.Products;
    using System.Data.Entity.ModelConfiguration;

    public class ProductTemplateConfiguration : EntityTypeConfiguration<ProductTemplate>
    {
        public ProductTemplateConfiguration()
        {
            this.ToTable("ProductTemplate");
            this.HasKey(pt => pt.Id);
            this.Property(pt => pt.Name).IsRequired();
        }
    }
}