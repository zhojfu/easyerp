namespace Doamin.Service.StoreSale
{
    using System.Collections.Generic;
    using Domain.Model.Orders;
    using Infrastructure.Utility;

    public interface IStoreSaleService
    {
        void DeleteOrderByIds(IEnumerable<int> ids);
        void UpdateOrder(Order order);
        void AddOrder(Order order);
        Order GetOrderById(int id);
        PagedResult<Order> GetOrders(int pageNumber, int pageSize);
    }
}