namespace Domain.EntityFramework.Configurations.Factory
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Factory;

    internal class AttritionConfiguration : EntityTypeConfiguration<Attrition>
    {
        public AttritionConfiguration()
        {
            HasKey(a => a.Id);
            Property(a => a.Name);
            Property(a => a.Unit);
            Property(a => a.Cost);
        }
    }
}