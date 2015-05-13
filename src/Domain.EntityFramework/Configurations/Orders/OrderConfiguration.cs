namespace Domain.EntityFramework.Configurations.Orders
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Orders;

    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            HasKey(o => o.Id);
            HasOptional(o => o.Customer).WithMany().HasForeignKey(o => o.CustomerId);//.WillCascadeOnDelete();
            HasOptional(o => o.Store).WithMany().HasForeignKey(o => o.StoreId);//.WillCascadeOnDelete();
        }
    }
}