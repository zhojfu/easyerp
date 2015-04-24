namespace EasyERP.Web.Framework.Mvc.Routes
{
    using System.Web.Routing;

    public interface IRoutePublisher
    {
        void RegisterRoutes(RouteCollection routes);
    }
}