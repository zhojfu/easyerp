namespace Domain.EntityFramework.Configurations.Products.Price
{
    using Domain.Model.Products.Price;
    using System.Data.Entity.ModelConfiguration;

    public class PriceListConfiguration : EntityTypeConfiguration<ProductPriceList>
    {
        public PriceListConfiguration()
        {
            this.ToTable("ProductPriceList");
            this.HasKey(pl => pl.Id);
            //this.Property(pl => pl.Name).IsRequired();
            this.HasOptional(pl => pl.Type).WithMany().HasForeignKey(pl => pl.PriceListTypeId);
            this.Property(pl => pl.Active);
            this.HasOptional(pl => pl.Company).WithMany().HasForeignKey(pl => pl.CompanyId);
        }
    }
}