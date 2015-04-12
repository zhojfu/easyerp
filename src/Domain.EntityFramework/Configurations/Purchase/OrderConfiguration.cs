namespace Domain.EntityFramework.Configurations.Purchase
{
    using Domain.Model.Purchase;
    using System.Data.Entity.ModelConfiguration;

    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            this.ToTable("PurchaseOrder");
            this.HasKey(o => o.Id);
            //this.Property(o => o.Name).IsRequired();
            this.Property(o => o.Origin);
            this.Property(o => o.PartnerRef);
            this.Property(o => o.OrderDate).IsRequired().HasColumnType("datetime2");
            this.Property(o => o.ApproveDate).IsRequired().HasColumnType("date");

            this.HasRequired(o => o.PriceList).WithMany().HasForeignKey(o => o.PriceListId);
            this.Property(o => o.State);

            this.HasOptional(o => o.Validator).WithMany().HasForeignKey(o => o.ValidatorId);
            this.Property(o => o.Notes);
        }
    }
}