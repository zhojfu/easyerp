namespace Domain.EntityFramework
{
    using Infrastructure.Domain;
    using Infrastructure.Domain.EntityFramework;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class EntityFrameworkRepository<TAggregateRoot> : BaseRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {
        private readonly IEntityFrameworkDbContext dbContext;

        public EntityFrameworkRepository(IEntityFrameworkDbContext dbcontext, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this.dbContext = dbcontext;
        }

        public override TAggregateRoot GetByKey(Guid key)
        {
            return this.dbContext.Set<TAggregateRoot>().First(p => p.Id == key);
        }

        public override IEnumerable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> expression)
        {
            return this.dbContext.Set<TAggregateRoot>().Where(expression);
        }

        public override bool Exist(TAggregateRoot aggregateRoot)
        {
            return this.dbContext.Set<TAggregateRoot>().Any(p => p.Equals(aggregateRoot));
        }

        public override void PersistNewItem(IAggregateRoot entity)
        {
            this.dbContext.Set<TAggregateRoot>().Add((TAggregateRoot)entity);
        }

        public override void PersistUpdateItem(IAggregateRoot entity)
        {
            ((DbContext)this.dbContext).Entry((TAggregateRoot)entity).State = EntityState.Modified;
        }

        public override void PersistRemoveItem(IAggregateRoot entity)
        {
            this.dbContext.Set<TAggregateRoot>().Remove((TAggregateRoot)entity);
        }
    }
}