namespace Domain.EntityFramework.Configurations.Products
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Products;

    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            ToTable("Category");
            HasKey(c => c.Id);
            Property(c => c.ItemNo).IsRequired();
            Property(c => c.Name).IsRequired().HasMaxLength(400);
        }
    }
}