namespace Infrastructure.Domain
{
    using Infrastructure.Domain.Model;

    public interface IUnitOfWork
    {
        void RegisterAdd(IAggregateRoot entity, IUnitOfWorkRepository repository);

        void RegisterUpdate(IAggregateRoot entity, IUnitOfWorkRepository repository);

        void RegisterRemoved(IAggregateRoot entity, IUnitOfWorkRepository repository);

        void Commit();
    }
}