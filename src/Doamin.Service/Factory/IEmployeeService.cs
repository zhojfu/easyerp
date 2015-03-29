
namespace Doamin.Service.Factory
{
    using System.Collections.Generic;

    using Domain.Model;

    public interface IEmployeeService
    {
        Employee GetEmployeeByName(string name);
        
        void AddEmployee(Employee employee);

        void DeleteEmployee(Employee employee);

        void UpdateEmployee(Employee employee);

        IEnumerable<Employee> GetEmployees();
    }
}
