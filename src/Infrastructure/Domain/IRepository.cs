
namespace Infrastructure.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Infrastructure.Utility;

    public enum SortOrder
    {
        Ascending,
        Descending,
        Unspecified// don't use this parameter if return pageresult, because the skip must after order
    }

    public interface IRepository<TAggregateRoot>
    {
        TAggregateRoot GetByKey(Guid key);

        IEnumerable<TAggregateRoot> FindAll();

        IEnumerable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> whereExp);

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