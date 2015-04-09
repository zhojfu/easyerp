using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyERP.Web.Controllers
{
    using System.Web.Helpers;
    using System.Web.Services.Description;

    using Doamin.Service.Factory;

    using Domain.Model.Factory;
    using EasyERP.Web.Models;

    public class TimesheetController : Controller
    {
        private readonly IEmployeeTimesheetService timeSheetService;

        private readonly IEmployeeService employeeService;

        public TimesheetController(IEmployeeTimesheetService timeSheetService, IEmployeeService employeeService)
        {
            this.timeSheetService = timeSheetService;
            this.employeeService = employeeService;
        }

        // GET: /WorkSheet/
        public ActionResult Index()
        {
            //this.timeSheetService.GetStatisticsByDate(DateTime.Now);
            return View();
        }


        public JsonResult GetTimeSheetByDate(string date, int page, int pageSize)
        {
            DateTime selectDate;

            if (!DateTime.TryParse(date, out selectDate))
            {
                return null;
            }

            var employees = this.employeeService.GetEmployees(page, pageSize);

            List<TimesheetModel> timesheetModels = new List<TimesheetModel>();

            foreach (var employee in employees)
            {
                var timeSheets = this.timeSheetService.GetEmployeeTimesheetByDate(employee.Id, selectDate);
                var model = new TimesheetModel
                {
                    Id = employee.Id.ToString(),
                    EmployeeName = employee.LastName + employee.FirstName
                };
                foreach (var workTime in timeSheets)
                {
                    
                    switch (workTime.Date.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            model.Mon = workTime.WorkTimeHr;
                            break;
                        case DayOfWeek.Tuesday:
                            model.Tue = workTime.WorkTimeHr;
                            break;
                        case DayOfWeek.Wednesday:
                            model.Wed = workTime.WorkTimeHr;
                            break;
                        case DayOfWeek.Thursday:
                            model.Thu = workTime.WorkTimeHr;
                            break;
                        case DayOfWeek.Friday:
                            model.Fri = workTime.WorkTimeHr;
                            break;
                        case DayOfWeek.Saturday:
                            model.Sat = workTime.WorkTimeHr;
                            break;
                        case DayOfWeek.Sunday:
                            model.Sun = workTime.WorkTimeHr;
                            break;
                    }
                }
                timesheetModels.Add(model);
            }

            return Json(new 
            {
                total = employees.TotalRecords,
                data = timesheetModels
            }, JsonRequestBehavior.AllowGet);
        }
    }
}