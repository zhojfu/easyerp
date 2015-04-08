
namespace Doamin.Service.Factory
{
    using System;
    using System.Collections.Generic;
    using Domain.Model;

    using Infrastructure.Domain;
    using Infrastructure.Utility;

    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> repository;

        private readonly IUnitOfWork unitOfWork;

        public EmployeeService(IRepository<Employee> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public Employee GetEmployeeById(Guid id)
        {
            return this.repository.GetByKey(id);
        }

        public void AddEmployee(Employee employee)
        {
            this.repository.Add(employee);
            this.unitOfWork.Commit();
        }

        public void DeleteEmployeeByIds(List<string> ids)
        {
            foreach (var id in ids)
            {
                var e = this.repository.GetByKey(new Guid(id));
                if (e != null)
                {
                    this.repository.Remove(e);
                }
            }

            this.unitOfWork.Commit();
        }

        public void UpdateEmployee(Employee employee)
        {
            this.repository.Update(employee);
            this.unitOfWork.Commit();
        }

        public PagedResult<Employee> GetEmployees(int pageNumber, int pageSize)
        {
            return this.repository.FindAll(pageSize, pageNumber, e => true, m => m.LastName, SortOrder.Ascending);
        }
    }
}
