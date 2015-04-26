namespace EasyERP.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using EasyERP.Web.Framework.Mvc.Routes;

    public class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "HomePage",
                "",
                new
                {
                    controller = "User",
                    action = "Login"
                },
                new[] { "EasyERP.Web.Controllers" });
        }

        public int Priority
        {
            get { return 0; }
        }
    }
}