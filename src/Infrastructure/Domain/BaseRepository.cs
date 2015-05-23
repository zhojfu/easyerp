namespace Infrastructure.Domain
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Infrastructure.Domain.Model;
    using Infrastructure.Utility;

    public abstract class BaseRepository<TAggregateRoot> : IRepository<TAggregateRoot>, IUnitOfWorkRepository
        where TAggregateRoot : IAggregateRoot
    {
        private readonly IUnitOfWork unitOfWork;

        protected BaseRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Add(TAggregateRoot aggregateRoot)
        {
            RegisterAdd(aggregateRoot);
        }

        public void Update(TAggregateRoot aggregateRoot)
        {
            RegisterUpdate(aggregateRoot);
        }

        public void Remove(TAggregateRoot aggregateRoot)
        {
            RegisterRemoved(aggregateRoot);
        }

        public void Update()
        {
            unitOfWork.Commit();
        }

        public abstract TAggregateRoot GetByKey(long key);
        public abstract IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> expression);
        public abstract bool Exist(TAggregateRoot aggregateRoot);

        public abstract PagedResult<TAggregateRoot> FindAll<TCol>(
            int pageSize,
            int pageNumber,
            Expression<Func<TAggregateRoot, bool>> selectExp,
            Expression<Func<TAggregateRoot, TCol>> orderExp,
            SortOrder sortOrder);

        public abstract void PersistNewItem(IAggregateRoot item);
        public abstract void PersistUpdateItem(IAggregateRoot item);
        public abstract void PersistRemoveItem(IAggregateRoot item);

        #region Wrapper UnitofWork Implementation

        protected void RegisterAdd(IAggregateRoot entity)
        {
            unitOfWork.RegisterAdd(entity, this);
        }

        protected void RegisterUpdate(IAggregateRoot entity)
        {
            unitOfWork.RegisterUpdate(entity, this);
        }

        protected void RegisterRemoved(IAggregateRoot entity)
        {
            unitOfWork.RegisterRemoved(entity, this);
        }

        #endregion Wrapper UnitofWork Implementation
    }
}