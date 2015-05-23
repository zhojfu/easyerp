namespace Doamin.Service.StoreSale
{
    using System.Collections.Generic;
    using Domain.Model.Orders;
    using Infrastructure.Utility;

    public interface IStoreSaleService
    {
        /*Customer GetCustomerById(int id);
        void AddCustomer(Customer customer);

        void DeleteCustomerByIds(List<int> ids);
        void UpdateCustomer(Customer customer);*/

        void DeleteOrderByIds(IEnumerable<int> ids);
        void UpdateOrder(Order order);
        void AddOrder(Order order);
        PagedResult<Order> GetOrders(int pageNumber, int pageSize);
    }
}