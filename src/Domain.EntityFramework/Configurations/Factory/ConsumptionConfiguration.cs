namespace Domain.EntityFramework.Configurations.Factory
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Factory;

    internal class ConsumptionConfiguration : EntityTypeConfiguration<Consumption>
    {
        public ConsumptionConfiguration()
        {
            HasKey(a => a.Id);
            Property(a => a.Name);
            Property(a => a.Unit);
            Property(a => a.PriceOfUnit);
        }
    }
}
