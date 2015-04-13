using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyERP.Web.Controllers
{
    using Doamin.Service.Security;
    using Domain.Model.Orders;

    public class ShoppingCartController : Controller
    {
        private readonly IPermissionService permissionService;

        public ShoppingCartController(IPermissionService permission)
        {
            this.permissionService = permission;
        }

        public ActionResult Index()
        {
            if (!permissionService.Authorize(StandardPermissionProvider.EnableShoppingCart))
            {
                return RedirectToRoute("HomePage");
            }
            return RedirectToAction("Cart");
        }

        public ActionResult Cart()
        {
            if (!permissionService.Authorize(StandardPermissionProvider.EnableShoppingCart))
            {
                return RedirectToRoute("HomePage");
            }

            var model = new ShoppingCartItemModel();
            return View(model);
        }
    }
}