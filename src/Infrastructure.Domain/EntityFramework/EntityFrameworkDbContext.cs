using System.Data.Entity;
using Infrastructure.Domain.Model;

namespace Infrastructure.Domain.EntityFramework
{
    public class EntityFrameworkDbContext : DbContext, IEntityFrameworkDbContext
    {
        private readonly ISetupConfiguration _configuration;

        public EntityFrameworkDbContext(string connectionString, ISetupConfiguration configuration)
            : base(connectionString)
        {
            this._configuration = configuration;
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class, IAggregateRoot
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this._configuration.SetupEntityConfiguration(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
