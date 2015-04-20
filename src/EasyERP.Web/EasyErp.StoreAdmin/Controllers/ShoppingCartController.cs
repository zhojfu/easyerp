namespace EasyErp.StoreAdmin.Controllers
{
    using Doamin.Service.Security;
    using EasyErp.StoreAdmin.Models.Order;
    using System.Web.Mvc;

    public class ShoppingCartController : Controller
    {
        private readonly IPermissionService permissionService;

        public ShoppingCartController(IPermissionService permission)
        {
            this.permissionService = permission;
        }

        public ActionResult Index()
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.EnableShoppingCart))
            {
                return this.RedirectToRoute("HomePage");
            }
            return this.RedirectToAction("Cart");
        }

        public ActionResult Cart()
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.EnableShoppingCart))
            {
                return this.RedirectToRoute("HomePage");
            }

            var model = new ShoppingCartItemModel();
            return this.View(model);
        }
    }
}