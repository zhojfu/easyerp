

namespace Domain.EntityFramework.Configurations.Products
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Products;

    public class ProductStoreMappingConfiguration : EntityTypeConfiguration<ProductStoreMapping>
    {
        public ProductStoreMappingConfiguration()
        {
            ToTable("Product");
            ToTable("Project_Store_Mapping");
            HasKey(pam => pam.Id);

            HasRequired(psm => psm.Product)
            .WithMany(p => p.ProductStoreMappings)
            .HasForeignKey(psm => psm.ProductId);

            HasRequired(psm => psm.Store)
                .WithMany(p=>p.ProductStoreMappings)
                .HasForeignKey(psm => psm.StoreId);
        }
    }
}