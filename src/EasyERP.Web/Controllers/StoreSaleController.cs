using System.Web.Mvc;

namespace EasyERP.Web.Controllers
{
    using EasyERP.Web.Models.StoreSale;

    public class StoreSaleController : Controller
    {
        //
        // GET: /StoreSales/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddOrderItem(OrderItemModel orderItem)
        {
            return Json(orderItem);
        }

        [HttpPost]
        public JsonResult DeleteOrderItem(OrderItemModel orderItem)
        {
            return Json(orderItem);
        }

        [HttpPost]
        public JsonResult UpdateOrderItem(OrderItemModel orderItem)
        {
            return Json(orderItem);
        }
	}
}