namespace Domain.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using Infrastructure.Domain;
    using Infrastructure.Domain.EntityFramework;
    using Infrastructure.Domain.Model;
    using Infrastructure.Utility;

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
            return this.dbContext.Set<TAggregateRoot>().FirstOrDefault(p => p.Id == key);
        }

        public override IEnumerable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> expression)
        {
            return this.GenerateSelectLinq(expression, null);
        }

        public override bool Exist(TAggregateRoot aggregateRoot)
        {
            return this.dbContext.Set<TAggregateRoot>().Any(p => p.Id == aggregateRoot.Id);
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

        public override PagedResult<TAggregateRoot> FindAll(
            int pageSize,
            int pageNumber,
            Expression<Func<TAggregateRoot, bool>> selectExp,
            Expression<Func<TAggregateRoot, dynamic>> orderExp,
            SortOrder sortOrder)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                throw new IndexOutOfRangeException("page size and page number is less than  zero");
            }

            var query = this.GenerateSelectLinq(selectExp, orderExp, sortOrder); 

            int skip = pageSize * (pageNumber - 1);
            int take = pageSize;
            var result = query.Skip(skip).Take(take).GroupBy(
                         p => new { Total = query.Count() }).FirstOrDefault();

            if (result != null)
            {
                return new PagedResult<TAggregateRoot>(
                    pageSize,
                    pageNumber,
                    (result.Key.Total + pageSize - 1) / pageSize,
                    result.Key.Total,
                    result.Select(t => t).ToList());
            }

            return null;
        }

        private IQueryable<TAggregateRoot> GenerateSelectLinq(
            Expression<Func<TAggregateRoot, bool>> selectExp,
            Expression<Func<TAggregateRoot, dynamic>> orderExp,
            SortOrder sortOrder = SortOrder.Unspecified)
        {
            var query = this.dbContext.Set<TAggregateRoot>().Where(selectExp);

            switch (sortOrder)
            {
                case SortOrder.Descending:
                    query = query.OrderByDescending(orderExp);
                    break;
                case SortOrder.Ascending:
                    query = query.OrderBy(orderExp);
                    break;
                case SortOrder.Unspecified:
                    break;
            }

            return query;
        }
    }
}