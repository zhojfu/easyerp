using System.Web.Mvc;

namespace EasyERP.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Helpers;
    using AutoMapper;
    using Doamin.Service.Factory;
    using Domain.Model.Factory;
    using EasyERP.Web.Models.Employee;
    using EasyERP.Web.Models.Factory;
    using Infrastructure.Utility;

    public class ConsumptionController : Controller
    {
        private readonly IConsumptionService consumptionService;

        private readonly ITimesheetService<ConsumptionStatistic> timesheetService;

        public ConsumptionController(IConsumptionService consumptionService, ITimesheetService<ConsumptionStatistic> timesheetService)
        {
            this.consumptionService = consumptionService;
            this.timesheetService = timesheetService;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get(int page, int pageSize)
        {
            var consumptions = this.consumptionService.GetConsumptionCategories(page, pageSize);

            if (consumptions == null)
                return null;

            List<ConsumptionModel> models = consumptions.Select(Mapper.Map<Consumption, ConsumptionModel>).Where(model => model != null).ToList();

            return Json(
                new
                {
                    total = consumptions.TotalRecords,
                    data = models
                }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(ConsumptionModel model)
        {
            var consumption = Mapper.Map<ConsumptionModel, Consumption>(model);
            if (consumption != null)
            {
                this.consumptionService.UpdateConsumptionCategory(consumption);
            }

            return Json(model);
        }

        [HttpPost]
        public JsonResult Create(ConsumptionModel model)
        {
            var consumption = Mapper.Map<ConsumptionModel, Consumption>(model);
            if (consumption != null)
            {
                this.consumptionService.AddConsumptionCategory(consumption);
            }

            return Json(model);
        }

        [HttpPost]
        public JsonResult Delete(ConsumptionModel model)
        {
            var consumption = Mapper.Map<ConsumptionModel, Consumption>(model);
            if (consumption != null)
            {
                this.consumptionService.DeleteConsumptionCategory(consumption);
            }
            return Json(model);
        }

        public ActionResult Statistic()
        {
            return View();
        }

        public JsonResult GetStatistic(string date, int page, int pageSize)
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
        public JsonResult UpdateStatistic(TimesheetModel model)
        {
            DateTime selectedDate;

            if (!DateTime.TryParse(model.DateOfWeek, out selectedDate))
            {
                return null;
            }
            Timesheet timesheet =  Mapper.Map<TimesheetModel, Timesheet>(model);
            this.timesheetService.UpdateTimesheet(selectedDate, timesheet);
            return Json(model);
        }
	}
}