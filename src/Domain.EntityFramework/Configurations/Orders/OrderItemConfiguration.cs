namespace Domain.EntityFramework.Configurations.Orders
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Orders;

    public class OrderItemConfiguration : EntityTypeConfiguration<OrderItem>
    {
        public OrderItemConfiguration()
        {
            HasKey(o => o.Id);
            HasRequired(o => o.Order).WithMany().HasForeignKey(o => o.OrderId).WillCascadeOnDelete();
            HasRequired(o => o.Product).WithMany().HasForeignKey(o => o.ProductId);
        }
    }
}