namespace Domain.EntityFramework.Configurations.Products.Price
{
    using Domain.Model.Products.Price;
    using System.Data.Entity.ModelConfiguration;

    public class PriceTypeConfiguration : EntityTypeConfiguration<ProductPriceType>
    {
        public PriceTypeConfiguration()
        {
            this.ToTable("ProductPriceType");
            this.HasKey(pt => pt.Id);
            //this.Property(pt => pt.Name).IsRequired();
            this.Property(pt => pt.Active);
        }
    }
}