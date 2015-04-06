using System.Web.Mvc;

namespace EasyERP.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Helpers;
    using System.Web.Routing;
    using AutoMapper;
    using Doamin.Service.Factory;
    using Domain.Model;
    using EasyERP.Web.Models;
    using Infrastructure.Utility;

    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        // GET: /Employee/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(string id)
        {
            Employee e = this.employeeService.GetEmployeeById(new Guid(id));
            var model = Mapper.Map<Employee, EmployeeModel>(e);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeModel model)
        {
            var e = Mapper.Map<EmployeeModel, Employee>(model);
            this.employeeService.UpdateEmployee(e);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(EmployeeModel employee)
        {
            Employee e = Mapper.Map<EmployeeModel, Employee>(employee);
            this.employeeService.AddEmployee(e);

            return RedirectToAction("Index");
        }

        [Route("Employee/EmployeeList/{pageNumber:int}")]
        public JsonResult EmployeeList(int pageNumber)
        {
            const int PageSize = 10;
            PagedResult<Employee> employees = this.employeeService.GetEmployees(pageNumber, PageSize);
            if (employees != null)
            {
                List<EmployeeListModel> employeesList = new List<EmployeeListModel>();

                foreach (var employee in employees)
                {
                    EmployeeListModel model = Mapper.Map<Employee, EmployeeListModel>(employee);
                    model.Sex = employee.Male ? "男" : "女";
                    model.FullName = employee.LastName + employee.FirstName;
                    employeesList.Add(model);
                }

                return Json(
                    new
                    {
                        employees.PageSize,
                        employees.PageNumber,
                        employees.TotalPages,
                        employeesList
                    } ,JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }
	}
}