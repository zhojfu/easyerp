using Infrastructure.Domain.Model;

namespace Infrastructure.Domain
{
    public interface IUnitOfWorkRepository
    {
        void PersistNewItem(IAggregateRoot item);

        void PersistUpdateItem(IAggregateRoot item);

        void PersistRemoveItem(IAggregateRoot item);
    }
}
