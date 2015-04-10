namespace Domain.EntityFramework.Configurations.Products.Price
{
    using Domain.Model.Products.Price;
    using System.Data.Entity.ModelConfiguration;

    public class PriceListTypeConfiguration : EntityTypeConfiguration<ProductPriceListType>
    {
        public PriceListTypeConfiguration()
        {
            this.ToTable("ProductPriceList");
            this.HasKey(pl => pl.Id);
            //this.Property(pl => pl.Name).IsRequired();
            this.Property(pl => pl.Key).IsRequired();
        }
    }
}