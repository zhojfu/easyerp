namespace Domain.EntityFramework.Configurations.Factory
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Factory;

    internal class AttritionStatisticConfiguration : EntityTypeConfiguration<AttritionStatistic>
    {
        public AttritionStatisticConfiguration()
        {
            HasKey(o => o.Id);
            Property(o => o.Date);
            Property(o => o.Volume);
            Property(o => o.PriceOfUnit);
            HasRequired(o => o.Attrition).WithMany(a => a.AttritionRecords).HasForeignKey(o => o.AttritionId);
        }
    }
}