namespace Domain.EntityFramework.Configurations.Orders
{
    using Domain.Model.Orders;
    using System.Data.Entity.ModelConfiguration;

    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            HasKey(o => o.Id);
            Property(o => o.ApproveTime).HasColumnType("datetime2");
            Property(o => o.PaymentId).IsRequired();
            HasRequired(o => o.Customer).WithMany().HasForeignKey(o => o.CustomerId).WillCascadeOnDelete();
        }
    }
}