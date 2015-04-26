namespace Infrastructure.Domain.EntityFramework
{
    using System.Data.Entity;
    using Infrastructure.Domain.Model;

    public interface IEntityFrameworkDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class, IAggregateRoot;
    }
}