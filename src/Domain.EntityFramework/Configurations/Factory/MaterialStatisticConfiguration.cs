namespace Domain.EntityFramework.Configurations.Factory
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Factory;

    internal class MaterialStatisticConfiguration : EntityTypeConfiguration<MaterialStatisitc>
    {
        public MaterialStatisticConfiguration()
        {
            HasKey(m => m.Id);
            Property(m => m.Date);
            Property(m => m.ComsumeQuantity);
            HasRequired(m => m.Material).WithMany(p => p.MaterialComsumptions).HasForeignKey(m => m.MaterialId);
        }
    }
}