namespace EasyErp.StoreAdmin
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using EasyERP.Web.Framework.Mvc.Routes;

    public class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "AddProductToCart",
                "storeadmin/addproducttocart/{productId}/{quantity}",
                new
                {
                    controller = "ShoppingCart",
                    action = "AddProductToCart"
                },
                new
                {
                    productId = @"\d+",
                    quantity = @"\d+"
                },
                new[] { "EasyErp.StoreAdmin.Controllers" });
        }

        public int Priority
        {
            get { return 10; }
        }
    }
}