namespace Domain.EntityFramework.Configurations
{
    using Domain.Model;
    using System.Data.Entity.ModelConfiguration;

    public class OrderCompositionConfiguration : EntityTypeConfiguration<OrderComposition>
    {
        public OrderCompositionConfiguration()
        {
            this.HasKey(o => o.Id);
            this.Property(o => o.Quantity).IsRequired();
            this.HasRequired(o => o.Stock);
            this.HasRequired(o => o.Order).WithMany(r => r.CompositionProducts).HasForeignKey(o => o.OrderId);
        }
    }
}