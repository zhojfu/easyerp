namespace Infrastructure.Domain
{
    using Infrastructure.Utility;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public enum SortOrder
    {
        Ascending,
        Descending,
        Unspecified
    }

    public interface IRepository<TAggregateRoot>
    {
        TAggregateRoot GetByKey(long key);

        IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> expression);

        PagedResult<TAggregateRoot> FindAll(
            int pageSize,
            int pageNumber,
            Expression<Func<TAggregateRoot, bool>> selectExp,
            Expression<Func<TAggregateRoot, dynamic>> orderExp,
            SortOrder sortOrder = SortOrder.Unspecified);

        void Add(TAggregateRoot aggregateRoot);

        bool Exist(TAggregateRoot aggregateRoot);

        void Update(TAggregateRoot aggregateRoot);

        void Remove(TAggregateRoot aggregateRoot);

        void Update();
    }
}