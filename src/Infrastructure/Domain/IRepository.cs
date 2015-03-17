using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Infrastructure.Domain
{
    public interface IRepository<TAggregateRoot>
    {
        TAggregateRoot GetByKey(Guid key);
        IEnumerable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> expression);
        void Add(TAggregateRoot aggregateRoot);
        bool Exist(TAggregateRoot aggregateRoot);
        void Update(TAggregateRoot aggregateRoot);
        void Remove(TAggregateRoot aggregateRoot);
    }
}
