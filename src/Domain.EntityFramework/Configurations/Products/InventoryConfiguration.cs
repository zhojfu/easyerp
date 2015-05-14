namespace Domain.EntityFramework.Configurations.Products
{
    using Domain.Model.Products;
    using System.Data.Entity.ModelConfiguration;

    internal class InventoryConfiguration : EntityTypeConfiguration<Inventory>
    {
        public InventoryConfiguration()
        {
            HasRequired(i => i.Product).WithMany().HasForeignKey(i => i.ProductId);
            Property(i => i.PaymentId).IsRequired();
            HasRequired(i => i.Payment).WithOptional(p => p.Inventory);
        }
    }
}