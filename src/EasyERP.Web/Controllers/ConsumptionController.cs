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

    public class ConsumptionController : Controller
    {
        private readonly IConsumptionService consumptionService;

        private readonly ITimesheetService<ConsumptionStatistic> timesheetService;

        public ConsumptionController(
            IConsumptionService consumptionService,
            ITimesheetService<ConsumptionStatistic> timesheetService)
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
            var consumption = Mapper.Map<ConsumptionModel, Consumption>(model);
            if (consumption != null)
            {
                consumptionService.UpdateConsumptionCategory(consumption);
            }

            return Json(model);
        }

        [HttpPost]
        public JsonResult Create(ConsumptionModel model)
        {
            var consumption = Mapper.Map<ConsumptionModel, Consumption>(model);
            if (consumption != null)
            {
                consumptionService.AddConsumptionCategory(consumption);
            }

            return Json(model);
        }

        [HttpPost]
        public JsonResult Delete(ConsumptionModel model)
        {
            var consumption = Mapper.Map<ConsumptionModel, Consumption>(model);
            if (consumption != null)
            {
                consumptionService.DeleteConsumptionCategory(consumption);
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