namespace Domain.EntityFramework
{
    using Domain.EntityFramework.Configurations;
    using Infrastructure.Domain.EntityFramework;
    using System.Data.Entity;

    public class EntitySetupConfiguration : ISetupConfiguration
    {
        public void SetupEntityConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TestDoublesConfiguration());
        }
    }
}