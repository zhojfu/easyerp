namespace Domain.EntityFramework.Configurations.Products
{
    using Domain.Model.Products;
    using System.Data.Entity.ModelConfiguration;

    public class PriceListPartnerinfoConfiguration : EntityTypeConfiguration<ProductPriceListPartnerinfo>
    {
        public PriceListPartnerinfoConfiguration()
        {
            this.ToTable("PriceListPartnerInfo");
            this.HasKey(p => p.Id);
            //this.HasKey(p => p.Name);
            this.Property(p => p.MinQuantity).IsRequired();
            this.Property(p => p.Price).IsRequired();
            this.HasRequired(p => p.Supplierinfo).WithMany().HasForeignKey(p => p.SupplierInfoId).WillCascadeOnDelete();
        }
    }
}