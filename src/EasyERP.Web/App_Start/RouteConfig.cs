namespace EasyERP.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using EasyErp.Core.Infrastructure;
    using EasyERP.Web.Framework.Mvc.Routes;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var routePublisher = EngineContext.Current.Resolve<IRoutePublisher>();
            routePublisher.RegisterRoutes(routes);

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                },
                new[] { "EasyERP.Web.Controllers" }
                );
        }
    }
}