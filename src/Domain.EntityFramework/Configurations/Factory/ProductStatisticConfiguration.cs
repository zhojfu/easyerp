namespace Domain.EntityFramework.Configurations.Factory
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Factory;

    internal class ProductStatisticConfiguration : EntityTypeConfiguration<ProductStatistic>
    {
        public ProductStatisticConfiguration()
        {
            HasKey(p => p.Id);
            Property(p => p.QualifyQuaitity);
            Property(p => p.QualifyQuaitity);
            HasRequired(p => p.Product).WithMany(o => o.ProduceRecord).HasForeignKey(p => p.ProductId);
        }
    }
}