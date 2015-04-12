namespace Domain.EntityFramework.Configurations.Orders
{
    using Domain.Model.Orders;
    using System.Data.Entity.ModelConfiguration;

    public class OrderConfiguration : EntityTypeConfiguration<OrderItem>
    {
        public OrderConfiguration()
        {
            this.HasKey(o => o.Id);
        }
    }
}