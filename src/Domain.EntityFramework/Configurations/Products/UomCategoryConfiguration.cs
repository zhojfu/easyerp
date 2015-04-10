namespace Domain.EntityFramework.Configurations.Products
{
    using Domain.Model.Products;
    using System.Data.Entity.ModelConfiguration;

    public class UomCategoryConfiguration : EntityTypeConfiguration<ProductUomCategory>
    {
        public UomCategoryConfiguration()
        {
            this.ToTable("ProductUomCategory");
            this.HasKey(uc => uc.Id);
            //this.HasRequired(uc => uc.Name);
        }
    }
}