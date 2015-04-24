namespace EasyERP.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using AutoMapper;
    using Doamin.Service.Factory;
    using Domain.Model;
    using EasyERP.Web.Models;

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

        public ActionResult Edit(int id)
        {
            var e = employeeService.GetEmployeeById(id);
            if (e != null)
            {
                var model = Mapper.Map<Employee, EmployeeModel>(e);
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(EmployeeModel model)
        {
            var e = Mapper.Map<EmployeeModel, Employee>(model);
            if (e != null)
            {
                employeeService.UpdateEmployee(e);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(EmployeeModel employee)
        {
            var e = Mapper.Map<EmployeeModel, Employee>(employee);
            if (e != null)
            {
                employeeService.AddEmployee(e);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Delete(List<int> ids)
        {
            if (ids != null)
            {
                employeeService.DeleteEmployeeByIds(ids);
            }
            return Json(null);
        }

        //[Route("Employee/EmployeeList/{skip:int}/{take:int}/{page:int}/{pageSize:int}")]
        //[HttpPost]
        public JsonResult EmployeeList(int skip, int take, int page, int pageSize)
        {
            //const int pageSize = 10;
            //int page = 1;
            //Request["page"];
            var employees = employeeService.GetEmployees(page, pageSize);
            if (employees != null)
            {
                var employeesList = new List<EmployeeListModel>();

                foreach (var employee in employees)
                {
                    var model = Mapper.Map<Employee, EmployeeListModel>(employee);
                    model.Sex = employee.Male ? "男" : "女";
                    model.FullName = employee.LastName + employee.FirstName;
                    employeesList.Add(model);
                }

                return Json(
                    new
                    {
                        total = employees.TotalRecords,
                        data = employeesList
                    },
                    JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }
    }
}