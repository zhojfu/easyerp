namespace Domain.EntityFramework.Configurations
{
    using Domain.Model;
    using System.Data.Entity.ModelConfiguration;

    public class PriceConfiguration : EntityTypeConfiguration<Price>
    {
        public PriceConfiguration()
        {
            this.HasKey(p => p.Id);
            this.Property(p => p.SalePrice);
            this.Property(p => p.Discount);
            this.Property(p => p.UpdataTime);
            this.HasRequired(p => p.Product).WithMany(pp => pp.Prices).HasForeignKey(p => p.ProductId);
            this.HasMany(p => p.Orders).WithRequired(o => o.Price);
        }
    }
}