using System.Data.Entity;

namespace Infrastructure.Domain.EntityFramework
{
    public class EntityFrameworkUnitOfWork : UnitOfWork
    {
        private readonly DbContext _dbContext;

        public EntityFrameworkUnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Commit()
        {
            base.Commit();
            this._dbContext.SaveChanges();
        }
    }
}
