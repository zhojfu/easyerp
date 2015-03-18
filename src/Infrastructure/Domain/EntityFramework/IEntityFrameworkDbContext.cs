using System.Data.Entity;
using Infrastructure.Domain.Model;

namespace Infrastructure.Domain.EntityFramework
{
    public interface IEntityFrameworkDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class, IAggregateRoot;
    }
}
