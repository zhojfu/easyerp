namespace Doamin.Service.Customer
{
    using System;
    using System.Collections.Generic;
    using Domain.Model.Customer;
    using Infrastructure.Domain;
    using Infrastructure.Utility;

    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> repository;

        private readonly IUnitOfWork unitOfWork;

        public CustomerService(IRepository<Customer> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public Customer GetCustomerById(int id)
        {
            return repository.GetByKey(id);
        }

        public void AddCustomer(Customer customer)
        {
            repository.Add(customer);
            unitOfWork.Commit();
        }

        public void DeleteCustomerByIds(List<int> ids)
        {
            foreach (var id in ids)
            {
                var e = repository.GetByKey(id);
                if (e != null)
                {
                    repository.Remove(e);
                }
            }

            unitOfWork.Commit();
        }

        public IEnumerable<Customer> GetCustomersByName(string name)
        {
            return this.repository.FindAll(c=>c.Name.Contains(name));
        }

        public void UpdateCustomer(Customer customer)
        {
             Customer origin = GetCustomerById(customer.Id);
            customer.CreatedOn = origin.CreatedOn;
            customer.UpdatedOn = DateTime.Now;
            repository.Update(customer);
            unitOfWork.Commit();
        }

        public PagedResult<Customer> GetCustomers(int pageNumber, int pageSize)
        {
            return repository.FindAll(pageSize, pageNumber, e => true, m => m.Name, SortOrder.Ascending);
        }
    }
}