namespace Domain.EntityFramework.Configurations.Orders
{
    using Domain.Model.Orders;
    using System.Data.Entity.ModelConfiguration;

    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            this.HasKey(o => o.Id);
        }
    }
}