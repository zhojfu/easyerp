using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Infrastructure.Domain.Model;

namespace Infrastructure.Domain.EntityFramework
{
    public class EntityFrameworkRepository<TAggregateRoot> : BaseRepository<TAggregateRoot> where TAggregateRoot : class, IAggregateRoot
    {
        private readonly IEntityFrameworkDbContext _dbContext;

        public EntityFrameworkRepository(IEntityFrameworkDbContext dbcontext, IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            this._dbContext = dbcontext;
        } 

        public override TAggregateRoot GetByKey(Guid key)
        {
            return this._dbContext.Set<TAggregateRoot>().First(p => p.Id == key);
        }

        public override IEnumerable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> expression)
        {
            return this._dbContext.Set<TAggregateRoot>().Where(expression);
        }

        public override bool Exist(TAggregateRoot aggregateRoot)
        {
            return this._dbContext.Set<TAggregateRoot>().Count(p => p.Id == aggregateRoot.Id) != 0;
        }

        public override void PersistNewItem(IAggregateRoot entity)
        {
            this._dbContext.Set<TAggregateRoot>().Add((TAggregateRoot)entity);
        }

        public override void PersistUpdateItem(IAggregateRoot entity)
        {
            ((DbContext)this._dbContext).Entry((TAggregateRoot)entity).State = EntityState.Modified;
        }

        public override void PersistRemoveItem(IAggregateRoot entity)
        {
            this._dbContext.Set<TAggregateRoot>().Remove((TAggregateRoot)entity);
        }
    }
}
