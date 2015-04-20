
namespace Domain.EntityFramework.Configurations.Factory
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Factory;

    internal class AttritionStatisticConfiguration : EntityTypeConfiguration<ConsumptionStatistic>
    {
        public AttritionStatisticConfiguration()
        {
            this.HasKey(o => o.Id);
            this.Property(o => o.Date);
            this.Property(o => o.Volume);
            this.Property(o => o.PriceOfUnit);
            this.HasRequired(o => o.Consumption).WithMany(a => a.ConsumptionRecords).HasForeignKey(o => o.ConsumptionId);
        }
    }
}
