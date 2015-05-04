namespace Domain.EntityFramework.Configurations.Store
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Stores;

    public class Storefiguration : EntityTypeConfiguration<Store>
    {
        public Storefiguration()
        {
            HasKey(s => s.Id);
        }
    }
}