namespace Doamin.Service.Customer
{
    using System.Collections.Generic;
    using Domain.Model.Customer;
    using Infrastructure.Utility;

    public interface ICustomerService
    {
        Customer GetCustomerById(int id);
        IEnumerable<Customer> GetCustomersByName(string name);
        void AddCustomer(Customer customer);
        void DeleteCustomerByIds(List<int> ids);
        void UpdateCustomer(Customer customer);
        PagedResult<Customer> GetCustomers(int pageNumber, int pageSize);
    }
}