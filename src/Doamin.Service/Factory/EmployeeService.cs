
namespace Doamin.Service.Factory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            //return this.repository.FindAll(m => m.Id == id).FirstOrDefault();
        }

        public void AddEmployee(Employee employee)
        {
            this.repository.Add(employee);
            this.unitOfWork.Commit();
        }

        public void DeleteEmployee(Employee employee)
        {
            this.repository.Remove(employee);
            this.unitOfWork.Commit();
        }

        public void UpdateEmployee(Employee employee)
        {
            //var origion = this.repository.GetByKey(employee.Id);
            
            this.repository.Update(employee);
            this.unitOfWork.Commit();
        }

        public PagedResult<Employee> GetEmployees(int pageNumber, int pageSize)
        {
           
           //var a1= this.repository.FindAll(m => true).ToList();
           //var a2=  this.repository.FindAll().ToList();
            return this.repository.FindAll(pageSize, pageNumber, e => true, m => m.LastName, SortOrder.Ascending);
        }
    }
}
