namespace EasyERP.Web
{
    using EasyERP.Web.Framework.Mvc.Routes;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "root",
                "",
                new
                {
                    controller = "User",
                    action = "Login"
                },
                new[] { "EasyERP.Web.Controllers" });

            routes.MapRoute(
                "HomePage",
                "HomePage",
                new
                {
                    controller = "User",
                    action = "Login"
                },
                new[] { "EasyERP.Web.Controllers" });

            routes.MapRoute(
                "Order",
                "Order/Review/{orderGuid}",
                new { controller = "Order", action = "Review" },
                new { orderGuid = new GuidConstraint(false) },
                new[] { "EasyERP.Web.Controllers" });
        }

        public int Priority
        {
            get { return 0; }
        }
    }
}