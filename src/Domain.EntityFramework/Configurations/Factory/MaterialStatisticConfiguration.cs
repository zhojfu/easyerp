namespace Domain.EntityFramework.Configurations.Factory
{
    using Domain.Model.Factory;
    using System.Data.Entity.ModelConfiguration;

    internal class MaterialStatisticConfiguration : EntityTypeConfiguration<MaterialStatisitc>
    {
        public MaterialStatisticConfiguration()
        {
            this.HasKey(m => m.Id);
            this.Property(m => m.Date);
            this.Property(m => m.ComsumeQuantity);

            //this.HasRequired(m => m.Material).WithMany(p => p.MaterialComsumptions).HasForeignKey(m => m.MaterialId);
        }
    }
}