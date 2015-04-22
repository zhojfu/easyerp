namespace Infrastructure.Domain.EntityFramework
{
    using System.Data.Entity;

    public interface ISetupConfiguration
    {
        void SetupEntityConfiguration(DbModelBuilder modelBuilder);
    }
}