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
                "OrderItems",
                "OrderItems/{orderGuid}",
                new { controller ="Order", action="OrderItems", orderGuid = UrlParameter.Optional},
                new[] { "EasyERP.Web.Controllers" });
            
            routes.MapRoute(
                "AddItem",
                "AddItem/{orderGuid}",
                new { controller ="Order", action="AddItem", orderGuid = UrlParameter.Optional},
                new[] { "EasyERP.Web.Controllers" });

            routes.MapRoute(
                "UpdateItems",
                "UpdateItems{orderGuid}",
                new { controller ="Order", action="UpdateItems", orderGuid = UrlParameter.Optional},
                new[] { "EasyERP.Web.Controllers" });
        }

        public int Priority
        {
            get { return 0; }
        }
    }
}