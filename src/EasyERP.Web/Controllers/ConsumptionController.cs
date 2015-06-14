using Doamin.Service.Security;
using EasyErp.Core;

namespace EasyERP.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Doamin.Service.Factory;
    using Domain.Model.Factory;
    using EasyERP.Web.Models.Employee;
    using EasyERP.Web.Models.Factory;

    public class ConsumptionController : BaseAdminController
    {
        private readonly IConsumptionService consumptionService;

        private readonly ITimesheetService<ConsumptionStatistic> timesheetService;

        private readonly IPermissionService permissionService;

        private readonly IWorkContext workContext;

        public ConsumptionController(
            IPermissionService permissionService,
            IConsumptionService consumptionService,
            ITimesheetService<ConsumptionStatistic> timesheetService,
            IWorkContext workContext)
        {
            this.consumptionService = consumptionService;
            this.timesheetService = timesheetService;
            this.permissionService = permissionService;
            this.workContext = workContext;
        }

        public ActionResult Index()
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.GetConsumption))
            {
                return AccessDeniedView();
            }

            return View();
        }

        public JsonResult Get(int page, int pageSize)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.GetConsumption))
            {
                return AccessDeniedJson();
            }

            var consumptions = consumptionService.GetConsumptionCategories(page, pageSize);

            if (consumptions == null)
            {
                return null;
            }

            var models =
                Enumerable.Where(consumptions.Select(Mapper.Map<Consumption, ConsumptionModel>), model => model != null)
                          .ToList();

            return Json(
                new
                {
                    total = consumptions.TotalRecords,
                    data = models
                },
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(ConsumptionModel model)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.UpdateConsumptionRecord))
            {
                return AccessDeniedJson();
            }

            var consumption = Mapper.Map<ConsumptionModel, Consumption>(model);
            if (consumption != null)
            {
                consumption.StoreId = workContext.CurrentUser.StoreId;
                consumptionService.UpdateConsumptionCategory(consumption);
            }

            return Json(model);
        }

        [HttpPost]
        public JsonResult Create(ConsumptionModel model)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.CreateConsumptionRecord))
            {
                return AccessDeniedJson();
            }

            var consumption = Mapper.Map<ConsumptionModel, Consumption>(model);
            if (consumption != null)
            {
                consumption.StoreId = workContext.CurrentUser.StoreId;
                consumptionService.AddConsumptionCategory(consumption);
            }

            return Json(model);
        }

        [HttpPost]
        public JsonResult Delete(ConsumptionModel model)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.DeleteConsumptionRecord))
            {
                return AccessDeniedJson();
            }

            var consumption = Mapper.Map<ConsumptionModel, Consumption>(model);
            if (consumption != null)
            {
                consumptionService.DeleteConsumptionCategory(consumption);
            }
            return Json(model);
        }

        public ActionResult Statistic()
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.StatisticConsumption))
            {
                return AccessDeniedView();
            }

            return View();
        }

        public JsonResult GetStatistic(string date, int page, int pageSize)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.StatisticConsumption))
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
                Enumerable.Where(timesheet.Select(Mapper.Map<Timesheet, TimesheetModel>), model => model != null)
                          .ToList();

            return Json(
                new
                {
                    data = result,
                    total = result.Count
                },
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateStatistic(TimesheetModel model)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.UpdateConsumptionRecord))
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