namespace Doamin.Service.StoreSale
{
    using System;
    using Domain.Model.Orders;
    using Infrastructure.Domain;
    using Infrastructure.Utility;

    public class StoreSaleService : IStoreSaleService
    {
        private readonly IRepository<Order> repository;

        private readonly IUnitOfWork unitOfWork;

        public StoreSaleService(IRepository<Order> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public PagedResult<Order> GetOrders(int pageNumber, int pageSize)
        {
            return repository.FindAll(pageSize, pageNumber, e => true, m => m.CreatedOnUtc, SortOrder.Descending);
        }

        public void DeleteOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void AddOrder(Order order)
        {
            repository.Add(order);
            unitOfWork.Commit();
        }
    }
}