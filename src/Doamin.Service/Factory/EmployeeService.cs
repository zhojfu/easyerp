
namespace Doamin.Service.Factory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Domain.Model;

    using Infrastructure.Domain;

    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> repository;

        private readonly IUnitOfWork unitOfWork;

        public EmployeeService(IRepository<Employee> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public Employee GetEmployeeByName(string idNumbr)
        {
            return this.repository.FindAll(m => m.IdNumber == idNumbr).FirstOrDefault();
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
            this.repository.Update(employee);
            this.unitOfWork.Commit();
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return null;
        }
    }
}
