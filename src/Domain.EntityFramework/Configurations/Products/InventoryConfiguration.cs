namespace Domain.EntityFramework.Configurations.Products
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Products;

    internal class InventoryConfiguration : EntityTypeConfiguration<Inventory>
    {
        public InventoryConfiguration()
        {
            HasRequired(i => i.Product).WithMany().HasForeignKey(i => i.ProductId);
            Property(i => i.PaymentId).IsRequired();
        }
    }
}