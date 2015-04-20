using System.Web.Mvc;

namespace EasyERP.Web.Controllers
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Doamin.Service.Factory;
    using Domain.Model;
    using Domain.Model.Factory;
    using EasyERP.Web.Models.Employee;
    using Infrastructure.Utility;
    using System.Collections.Generic;

    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        private readonly ITimesheetService timesheetService;

        public EmployeeController(IEmployeeService employeeService, ITimesheetService timesheetService)
        {
            this.employeeService = employeeService;
            this.timesheetService = timesheetService;
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

        public ActionResult Timesheet()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            Employee e = this.employeeService.GetEmployeeById(id);
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
                this.employeeService.UpdateEmployee(e);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(EmployeeModel employee)
        {
            Employee e = Mapper.Map<EmployeeModel, Employee>(employee);
            if (e != null)
            {
                this.employeeService.AddEmployee(e);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Delete(List<int> ids)
        {
            if (ids != null)
            {
                this.employeeService.DeleteEmployeeByIds(ids);
            }
            return Json(null);
        }

        public JsonResult EmployeeList(int skip, int take, int page, int pageSize)
        {
            PagedResult<Employee> employees = this.employeeService.GetEmployees(page, pageSize);
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
                        total = employees.TotalRecords,
                        data = employeesList
                    },
                    JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }
                          
        public JsonResult GeTimeSheetByDate(string date, int page, int pageSize)
        {
            DateTime selectedDate;

            if (!DateTime.TryParse(date, out selectedDate))
            {
                return null;
            }

            IEnumerable<Timesheet> timesheet = this.timesheetService.GetTimesheetByDate(page, pageSize, selectedDate);

            List<TimesheetModel> result = timesheet.Select(Mapper.Map<Timesheet, TimesheetModel>).Where(model => model != null).ToList();

            return Json(new { data = result, total = result.Count }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateTimesheet(TimesheetModel model)
        {
            DateTime selectedDate;

            if (!DateTime.TryParse(model.DateOfWeek, out selectedDate))
            {
                return null;
            }
            Timesheet timesheet = Mapper.Map<TimesheetModel, Timesheet>(model);
            this.timesheetService.UpdateTimesheet(selectedDate, timesheet);
            return Json(model);
        }
    }
}