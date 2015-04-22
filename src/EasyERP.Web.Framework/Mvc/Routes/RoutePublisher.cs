namespace EasyERP.Web.Framework.Mvc.Routes
{
    using EasyErp.Core.Infrastructure;
    using Nop.Core.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Routing;

    public class RoutePublisher : IRoutePublisher
    {
        private readonly ITypeFinder typeFinder;

        public RoutePublisher(ITypeFinder typeFinder)
        {
            this.typeFinder = typeFinder;
        }

        public virtual void RegisterRoutes(RouteCollection routes)
        {
            var routeProviderTypes = typeFinder.FindClassesOfType<IRouteProvider>();
            var routeProviders = new List<IRouteProvider>();
            foreach (var providerType in routeProviderTypes)
            {
                var provider = Activator.CreateInstance(providerType) as IRouteProvider;
                routeProviders.Add(provider);
            }
            routeProviders = routeProviders.OrderByDescending(rp => rp.Priority).ToList();
            routeProviders.ForEach(rp => rp.RegisterRoutes(routes));
        }
    }
}