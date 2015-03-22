namespace Domain.EntityFramework.Configurations
{
    using Domain.Model;
    using System.Data.Entity.ModelConfiguration;

    public class StoreConfiguration : EntityTypeConfiguration<Store>
    {
        public StoreConfiguration()
        {
            this.HasKey(s => s.Id);
            this.Property(s => s.Name);
            this.Property(s => s.Address);
            this.HasMany(s => s.Orders).WithRequired(o => o.Store);
        }
    }
}