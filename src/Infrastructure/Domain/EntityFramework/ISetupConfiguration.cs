using System.Data.Entity;

namespace Infrastructure.Domain.EntityFramework
{
    public interface ISetupConfiguration
    {
        void SetupEntityConfiguration(DbModelBuilder modelBuilder);
    }
}
