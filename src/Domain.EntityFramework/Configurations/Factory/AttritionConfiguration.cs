
namespace Domain.EntityFramework.Configurations.Factory
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Factory;

    internal class AttritionConfiguration : EntityTypeConfiguration<Consumption>
    {
        public AttritionConfiguration()
        {
            this.HasKey(a => a.Id);
            this.Property(a => a.Name);
            this.Property(a => a.Unit);
            this.Property(a => a.PriceOfUnit);
        }
    }
}
