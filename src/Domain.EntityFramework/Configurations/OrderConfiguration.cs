namespace Domain.EntityFramework.Configurations
{
    using Domain.Model;
    using System.Data.Entity.ModelConfiguration;

    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            this.HasKey(o => o.Id).ToTable("Orders");
            this.Property(o => o.Name).HasMaxLength(50);
            this.Property(o => o.Description).HasMaxLength(50);
            this.Property(o => o.ApplyDate);
            this.Property(o => o.ApprovalTime);
            this.Property(o => o.ReceiveTime);
            this.Property(o => o.Status);
            this.Property(o => o.Quantity);
            this.HasRequired(o => o.Store).WithMany(s => s.Orders).HasForeignKey(o => o.StoreId);
            this.HasRequired(o => o.Price).WithMany(p => p.Orders).HasForeignKey(o => o.PriceId);
            this.HasMany(o => o.CompositionProducts).WithRequired(c => c.Order);
            this.HasMany(o => o.PayInfos);
        }
    }
}