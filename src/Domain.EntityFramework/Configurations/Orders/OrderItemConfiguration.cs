namespace Domain.EntityFramework.Configurations.Orders
{
    using Domain.Model.Orders;
    using System.Data.Entity.ModelConfiguration;

    public class OrderItemConfiguration : EntityTypeConfiguration<OrderItem>
    {
        public OrderItemConfiguration()
        {
            this.HasKey(o => o.Id);
            this.HasRequired(o => o.Order).WithMany().HasForeignKey(o => o.OrderId).WillCascadeOnDelete();
            this.HasRequired(o => o.Product).WithMany().HasForeignKey(o => o.ProductId);
        }
    }
}