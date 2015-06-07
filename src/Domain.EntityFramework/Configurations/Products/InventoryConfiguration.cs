namespace Domain.EntityFramework.Configurations.Products
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Products;

    internal class InventoryConfiguration : EntityTypeConfiguration<Inventory>
    {
        public InventoryConfiguration()
        {
            HasKey(i => i.Id);
            Property(i => i.PaymentId).IsRequired();

            HasRequired(i => i.Product)
            .WithMany(p => p.ProductInventories)
            .HasForeignKey(i => i.ProductId);

            HasRequired(i => i.Store)
                .WithMany(p=>p.ProductInventories)
                .HasForeignKey(i => i.StoreId);
        }
    }
}