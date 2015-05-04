namespace Doamin.Service.Customer
{
    using System.Collections.Generic;
    using Domain.Model.Customer;
    using Infrastructure.Utility;

    public interface ICustomerService
    {
        Customer GetCustomerById(int id);
        void AddCustomer(Customer customer);

        void DeleteCustomerByIds(List<int> ids);
        void UpdateCustomer(Customer customer);
        PagedResult<Customer> GetCustomers(int pageNumber, int pageSize);
    }
}