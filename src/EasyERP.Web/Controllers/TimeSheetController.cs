using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyERP.Web.Controllers
{
    using System.Security.AccessControl;
    using System.Web.Helpers;
    using System.Web.Services.Description;

    using Doamin.Service.Factory;

    using Domain.Model.Factory;
    using EasyERP.Web.Models;
    using EasyERP.Web.Models.Employee;
    using Infrastructure.Utility;

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

        /*[HttpPost]
        public JsonResult UpdateTimesheet(TimesheetModel timesheet)
        {
            return null;
        }*/


        [HttpPost]
        public JsonResult UpdateTimesheet(TimesheetModel timesheet)
        {
            DateTime selectDate;

            if (!DateTime.TryParse(timesheet.DateOfWeek, out selectDate))
            {
                return null;
            }

            var dateRange = DateHelper.GetWeekRangeOfCurrentDate(selectDate);
            
            Dictionary<DateTime, double> worktimes = new Dictionary<DateTime, double>
            {
                { dateRange.Item1, timesheet.Mon },
                { dateRange.Item1.AddDays(1), timesheet.Tue },
                { dateRange.Item1.AddDays(2), timesheet.Wed },
                { dateRange.Item1.AddDays(3), timesheet.Thu },
                { dateRange.Item1.AddDays(4), timesheet.Fri },
                { dateRange.Item1.AddDays(5), timesheet.Sat },
                { dateRange.Item1.AddDays(6), timesheet.Sun }
            };

            this.timeSheetService.UpdateTimesheet(timesheet.Id, worktimes);
            return Json(timesheet);
        }

        public JsonResult GetTimeSheetByDate(string date, /*int take, int skip,*/ int page, int pageSize)
        {
            DateTime selectDate;

            var employees = this.employeeService.GetEmployees(page, pageSize);

            if (!DateTime.TryParse(date, out selectDate) || employees == null )
            {
                return null;
            }
            
            List<TimesheetModel> timesheetModels = new List<TimesheetModel>();

            foreach (var employee in employees)
            {
                var timeSheets = this.timeSheetService.GetEmployeeTimesheetByDate(employee.Id, selectDate);
                var model = new TimesheetModel
                {
                    Id = employee.Id,
                    DateOfWeek = date,
                    Title = employee.LastName + employee.FirstName
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