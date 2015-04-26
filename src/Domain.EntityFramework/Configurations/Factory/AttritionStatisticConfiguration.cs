namespace Domain.EntityFramework.Configurations.Factory
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Factory;

    internal class AttritionStatisticConfiguration : EntityTypeConfiguration<ConsumptionStatistic>
    {
        public AttritionStatisticConfiguration()
        {
            HasKey(o => o.Id);
            Property(o => o.Date);
            Property(o => o.Volume);
            Property(o => o.PriceOfUnit);
            HasRequired(o => o.Consumption).WithMany(a => a.ConsumptionRecords).HasForeignKey(o => o.ConsumptionId);
        }
    }
}
