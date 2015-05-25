namespace EasyERP.Web.Framework.Mvc.Routes
{
    using System.Web.Routing;

    public interface IRouteProvider
    {
        int Priority { get; }

        void RegisterRoutes(RouteCollection routes);
    }
}