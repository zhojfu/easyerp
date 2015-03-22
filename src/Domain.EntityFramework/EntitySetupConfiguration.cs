namespace Domain.EntityFramework
{
    using System.Data.Entity;
    using Domain.EntityFramework.Configurations;
    using Infrastructure.Domain.EntityFramework;

    public class EntitySetupConfiguration : ISetupConfiguration
    {
        public void SetupEntityConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TestDoublesConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
        }
    }
}
