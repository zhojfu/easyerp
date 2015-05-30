using Doamin.Service.Security;
using EasyErp.Core;

namespace EasyERP.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Doamin.Service.Factory;
    using Domain.Model;
    using Domain.Model.Factory;
    using EasyERP.Web.Models.Employee;
    using Infrastructure;

    public class EmployeeController :  BaseAdminController
    {
        private readonly IEmployeeService employeeService;
        private readonly IPermissionService permissionService;
        private readonly IWorkContext workContext;
        private readonly ITimesheetService<WorkTimeStatistic> timesheetService;

        public EmployeeController(
            IEmployeeService employeeService,
            IPermissionService permissionService,
            ITimesheetService<WorkTimeStatistic> timesheetService,
            IWorkContext workContext)
        {
            this.employeeService = employeeService;
            this.timesheetService = timesheetService;
            this.permissionService = permissionService;
            this.workContext = workContext;
        }

        // GET: /Employee/
        public ActionResult Index()
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageStores))
            {
                return AccessDeniedView();
            }
                
            return View();
        }

        public ActionResult Create()
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageStores))
            {
                return AccessDeniedView();
            }
            return View();
        }

        public ActionResult Timesheet()
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageStores))
            {
                return AccessDeniedView();
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageStores))
            {
                return AccessDeniedView();
            }

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
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageStores))
            {
                return AccessDeniedView();
            }

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
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageStores))
            {
                return AccessDeniedView();
            }

            var e = Mapper.Map<EmployeeModel, Employee>(employee);
            if (e != null)
            {
                e.StoreId = workContext.CurrentUser.StoreId;
                employeeService.AddEmployee(e);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Delete(List<int> ids)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageStores))
            {
                return AccessDeniedJson();
            }

            if (ids != null)
            {
                employeeService.DeleteEmployeeByIds(ids);
            }
            return Json(ids);
        }

        public JsonResult EmployeeList(int skip, int take, int page, int pageSize)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageStores))
            {
                return AccessDeniedJson();
            }

            var employees = employeeService.GetEmployees(page, pageSize);
            if (employees != null)
            {
                var employeesList = new List<EmployeeListModel>();

                foreach (var employee in employees)
                {
                    var model = Mapper.Map<Employee, EmployeeListModel>(employee);
                    model.Sex = employee.Male ? "男" : "女";
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
            return Json(new{}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTimeSheetByDate(string date, int page, int pageSize)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageStores))
            {
                return AccessDeniedJson();
            }

            DateTime selectedDate;

            if (!DateTime.TryParse(date, out selectedDate))
            {
                return null;
            }

            var timesheet = timesheetService.GetTimesheetByDate(page, pageSize, selectedDate);

            var result =
                timesheet.IfNotNull(
                    t =>
                    Enumerable.Where(t.Select(Mapper.Map<Timesheet, TimesheetModel>), model => model != null).ToList());
            
            if(result == null)
                return Json(null);

            return Json(
                new
                {
                    data = result,
                    total = result.Count
                },
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateTimesheet(TimesheetModel model)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageStores))
            {
                return AccessDeniedJson();
            }

            DateTime selectedDate;

            if (!DateTime.TryParse(model.DateOfWeek, out selectedDate))
            {
                return null;
            }
            var timesheet = Mapper.Map<TimesheetModel, Timesheet>(model);
            timesheetService.UpdateTimesheet(selectedDate, timesheet);
            return Json(model);
        }
    }
}