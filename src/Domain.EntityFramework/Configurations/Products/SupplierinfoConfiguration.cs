namespace Domain.EntityFramework.Configurations.Products
{
    using Domain.Model.Products;
    using System.Data.Entity.ModelConfiguration;

    public class SupplierinfoConfiguration : EntityTypeConfiguration<Supplierinfo>
    {
        public SupplierinfoConfiguration()
        {
            this.ToTable("ProductSupplierinfo");
            this.HasKey(s => s.Id);
            //this.Property(s => s.Name).IsRequired();
            this.Property(s => s.ProductName);
            this.Property(s => s.ProductCode);
            this.Property(s => s.Order);
            this.Property(s => s.MinQuantity).IsRequired();
            this.Property(s => s.Delay);
            this.HasRequired(s => s.ProductTemplate)
                .WithMany()
                .HasForeignKey(s => s.ProductTemplateId)
                .WillCascadeOnDelete();
            this.HasOptional(s => s.Company).WithMany().HasForeignKey(s => s.CompanyId);
        }
    }
}