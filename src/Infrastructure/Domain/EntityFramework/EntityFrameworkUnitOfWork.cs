using System.Data.Entity;

namespace Infrastructure.Domain.EntityFramework
{
    public class EntityFrameworkUnitOfWork : UnitOfWork
    {
        private readonly DbContext dbContext;

        public EntityFrameworkUnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public override void Commit()
        {
            base.Commit();
            this.dbContext.SaveChanges();
        }
    }
}
