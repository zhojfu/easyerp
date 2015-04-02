using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyERP.Web.Controllers
{
    using System.Web.Services.Description;

    using Doamin.Service.Factory;

    using Domain.Model.Factory;

    public class TimeSheetController : Controller
    {
        private IStatisticService<WorkTimeStatistic> timeSheetService;
 
        public TimeSheetController(IStatisticService<WorkTimeStatistic> timeSheetService)
        {
            this.timeSheetService = timeSheetService;
        }

        // GET: /WorkSheet/
        public ActionResult Index()
        {
            this.timeSheetService.GetStatisticsByDate(DateTime.Now);
            return View();
        }

        public JsonResult GetTimeSheetByDate(string date)
        {
            DateTime selectDate;
            if (DateTime.TryParse(date, out selectDate))
            {
                this.timeSheetService.GetStatisticsByDate(selectDate);
                
            }

            return null;
        }
	}
}