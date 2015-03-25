
namespace Domain.EntityFramework.Configurations.Factory
{
    using Domain.Model.Factory;
    using System.Data.Entity.ModelConfiguration;

    internal class ProductStatisticConfiguration : EntityTypeConfiguration<ProductStatistic>
    {
        public ProductStatisticConfiguration()
        {
            this.HasKey(p => p.Id);
            this.Property(p => p.QualifyQuaitity);
            this.Property(p => p.QualifyQuaitity);
            this.HasRequired(p => p.Product).WithMany(o => o.ProduceRecord).HasForeignKey(p => p.ProductId);
        }
    }
}
