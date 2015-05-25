namespace Domain.EntityFramework
{
    using System.Data.Entity;
    using Infrastructure.Domain;

    public class EntityFrameworkUnitOfWork : UnitOfWork
    {
        private readonly DbContext dbContext;

        public EntityFrameworkUnitOfWork(EntityFrameworkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public override void Commit()
        {
            base.Commit();
            dbContext.SaveChanges();
        }
    }
}