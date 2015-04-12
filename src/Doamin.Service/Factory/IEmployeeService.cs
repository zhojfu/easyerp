namespace Doamin.Service.Factory
{
    using Domain.Model;
    using Infrastructure.Utility;
    using System;
    using System.Collections.Generic;

    public interface IEmployeeService
    {
        Employee GetEmployeeById(int id);

        void AddEmployee(Employee employee);

        // void DeleteEmployee(Employee employee);

        void DeleteEmployeeByIds(List<int> ids);

        void UpdateEmployee(Employee employee);

        PagedResult<Employee> GetEmployees(int pageNumber, int pageSize);
    }
}