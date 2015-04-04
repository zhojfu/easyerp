using System.Web.Mvc;
using System.Web.Routing;

namespace EasyERP.Web
{
    using System.Web.Optimization;

    using EasyERP.Web.App_Start;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapperBootstraper.RegisterModelMapper();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
