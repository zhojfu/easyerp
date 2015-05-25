namespace Domain.EntityFramework.Configurations.Store
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Stores;

    public class StoreConfiguration : EntityTypeConfiguration<Store>
    {
        public StoreConfiguration()
        {
            HasKey(s => s.Id);
        }
    }
}