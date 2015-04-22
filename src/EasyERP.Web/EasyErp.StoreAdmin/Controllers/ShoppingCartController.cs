namespace EasyErp.StoreAdmin.Controllers
{
    using Doamin.Service.Order;
    using Doamin.Service.Products;
    using Doamin.Service.Security;
    using EasyErp.Core;
    using EasyErp.StoreAdmin.Models.Order;
    using System.Linq;
    using System.Web.Mvc;

    public class ShoppingCartController : Controller
    {
        private readonly IPermissionService permissionService;

        private readonly IProductService productService;

        private readonly IShoppingCartService shoppingCartService;

        private readonly IWorkContext workContext;

        public ShoppingCartController(
            IWorkContext workContext,
            IPermissionService permissionService,
            IProductService productService,
            IShoppingCartService shoppingCartService)
        {
            this.workContext = workContext;
            this.permissionService = permissionService;
            this.productService = productService;
            this.shoppingCartService = shoppingCartService;
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

        [HttpPost]
        public ActionResult AddProductToCart(int productId, int quantity, bool forceredirection = false)
        {
            var product = productService.GetProductById(productId);
            if (product == null)
            {
                return Json(
                    new
                    {
                        success = false,
                        message = "No product found with the specified ID"
                    });
            }

            //get standard warnings without attribute validations
            //first, try to find existing shopping cart item
            var cart = workContext.CurrentUser.ShoppingCartItems;

            var shoppingCartItem = shoppingCartService.FindShoppingCartItemInTheCart(cart.ToList(), product);

            //if we already have the same product in the cart, then use the total quantity to validate
            var quantityToValidate = shoppingCartItem != null ? shoppingCartItem.Quantity + quantity : quantity;

            shoppingCartService.AddToCart(workContext.CurrentUser, product, quantity);

            //display notification message and update appropriate blocks
            var updatetopcartsectionhtml =
                string.Format(
                    "({0})",
                    workContext.CurrentUser.ShoppingCartItems.ToList().Count);

            return Json(
                new
                {
                    success = true,
                    message =
                    string.Format(
                        @"<![CDATA[The product has been added to your <a href={0}>shopping cart</a>]]>",
                        Url.RouteUrl("ShoppingCart")),
                    updatetopcartsectionhtml
                });
        }
    }
}