namespace Domain.EntityFramework.Configurations.Products
{
    using Domain.Model.Products;
    using System.Data.Entity.ModelConfiguration;

    public class PriceHistoryConfiguration : EntityTypeConfiguration<ProductPriceHistory>
    {
        public PriceHistoryConfiguration()
        {
            this.ToTable("ProductPriceHistory");
            this.HasKey(ph => ph.Id);
            //this.Property(ph => ph.Name);
            this.Property(ph => ph.Cost);
            this.Property(ph => ph.DateTime);
            this.HasRequired(ph => ph.ProductTemplate)
                .WithMany()
                .HasForeignKey(ph => ph.ProductTemplateId)
                .WillCascadeOnDelete();
            this.HasRequired(ph => ph.Company).WithMany().HasForeignKey(ph => ph.CompanyId);
        }
    }
}