namespace Domain.EntityFramework.Configurations.Orders
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Orders;

    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            HasKey(o => o.Id);
            Property(o => o.ApproveTime).HasColumnType("datetime2");
            Property(o => o.PaidDateUtc).HasColumnType("datetime2");
            HasRequired(o => o.Customer).WithMany().HasForeignKey(o => o.CustomerId).WillCascadeOnDelete();
        }
    }
}