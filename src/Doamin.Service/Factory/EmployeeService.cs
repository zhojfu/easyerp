namespace Doamin.Service.Factory
{
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

        public Employee GetEmployeeById(int id)
        {
            return repository.GetByKey(id);
        }

        public void AddEmployee(Employee employee)
        {
            repository.Add(employee);
            unitOfWork.Commit();
        }

        public void DeleteEmployeeByIds(List<int> ids)
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

        public void UpdateEmployee(Employee employee)
        {
            repository.Update(employee);
            unitOfWork.Commit();
        }

        public PagedResult<Employee> GetEmployees(int pageNumber, int pageSize)
        {
            return repository.FindAll(pageSize, pageNumber, e => true, m => m.Name, SortOrder.Ascending);
        }
    }
}