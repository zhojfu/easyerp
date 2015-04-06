
namespace Doamin.Service.Factory
{
    using System;
    using Domain.Model;
    using Infrastructure.Utility;

    public interface IEmployeeService
    {
        Employee GetEmployeeById(Guid id);
        
        void AddEmployee(Employee employee);

        void DeleteEmployee(Employee employee);

        void UpdateEmployee(Employee employee);

        PagedResult<Employee> GetEmployees(int pageNumber, int pageSize);
    }
}
