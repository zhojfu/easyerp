namespace Domain.EntityFramework.Configurations.Products
{
    using Domain.Model.Products;
    using System.Data.Entity.ModelConfiguration;

    public class UomConfigration : EntityTypeConfiguration<ProductUom>
    {
        public UomConfigration()
        {
            this.ToTable("ProductUom");
            this.HasKey(u => u.Id);
            this.HasRequired(u => u.Category).WithMany().HasForeignKey(u => u.UomCategoryId);
            this.Property(u => u.Factor).IsRequired();
            this.Property(u => u.Rounding).IsRequired();
            this.Property(u => u.Active);
            this.Property(u => u.Type).IsRequired();
        }
    }
}