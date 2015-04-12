namespace Domain.EntityFramework.Configurations.Products.Price
{
    using Domain.Model.Products.Price;
    using System.Data.Entity.ModelConfiguration;

    public class PriceListVersionConfiguration : EntityTypeConfiguration<ProductPriceListVersion>
    {
        public PriceListVersionConfiguration()
        {
            this.ToTable("ProductPriceListVersion");
            this.HasKey(pl => pl.Id);
            //this.Property(pl => pl.Name).IsRequired();
            this.Property(pl => pl.Active);
            this.Property(pl => pl.StartDate);
            this.Property(pl => pl.EndDate);
        }
    }
}