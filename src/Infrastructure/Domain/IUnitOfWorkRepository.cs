namespace Infrastructure.Domain
{
    using Infrastructure.Domain.Model;

    public interface IUnitOfWorkRepository
    {
        void PersistNewItem(IAggregateRoot item);
        void PersistUpdateItem(IAggregateRoot item);
        void PersistRemoveItem(IAggregateRoot item);
    }
}