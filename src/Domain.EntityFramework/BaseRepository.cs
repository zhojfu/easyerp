namespace Domain.EntityFramework
{
    using Infrastructure.Domain;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

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
            this.RegisterAdd(aggregateRoot);
        }

        public void Update(TAggregateRoot aggregateRoot)
        {
            this.RegisterUpdate(aggregateRoot);
        }

        public void Remove(TAggregateRoot aggregateRoot)
        {
            this.RegisterRemoved(aggregateRoot);
        }

        public void Update()
        {
            this.unitOfWork.Commit();
        }

        public abstract TAggregateRoot GetByKey(Guid key);

        public abstract IEnumerable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> expression);

        public abstract bool Exist(TAggregateRoot aggregateRoot);

        public abstract void PersistNewItem(IAggregateRoot item);

        public abstract void PersistUpdateItem(IAggregateRoot item);

        public abstract void PersistRemoveItem(IAggregateRoot item);

        #region Wrapper UnitofWork Implementation

        protected void RegisterAdd(IAggregateRoot entity)
        {
            this.unitOfWork.RegisterAdd(entity, this);
        }

        protected void RegisterUpdate(IAggregateRoot entity)
        {
            this.unitOfWork.RegisterUpdate(entity, this);
        }

        protected void RegisterRemoved(IAggregateRoot entity)
        {
            this.unitOfWork.RegisterRemoved(entity, this);
        }

        #endregion Wrapper UnitofWork Implementation
    }
}