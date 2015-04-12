namespace Doamin.Service.Factory
{
    using Domain.Model;
    using Infrastructure.Domain;
    using Infrastructure.Utility;
    using System;
    using System.Collections.Generic;

    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> repository;

        private readonly IUnitOfWork unitOfWork;

        public EmployeeService(IRepository<Employee> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public Employee GetEmployeeById(int id)
        {
            return this.repository.GetByKey(id);
        }

        public void AddEmployee(Employee employee)
        {
            this.repository.Add(employee);
            this.unitOfWork.Commit();
        }

        public void DeleteEmployeeByIds(List<int> ids)
        {
            foreach (var id in ids)
            {
                var e = this.repository.GetByKey(id);
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