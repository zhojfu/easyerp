namespace Domain.EntityFramework.Configurations.Store
{
    using Domain.Model.Stores;
    using System.Data.Entity.ModelConfiguration;

    public class StoreConfiguration : EntityTypeConfiguration<Store>
    {
        public StoreConfiguration()
        {
            this.HasKey(s => s.Id);
        }
    }
}