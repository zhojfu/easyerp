namespace EasyERP.Web.Framework
{
    using Autofac;
    using Autofac.Core;
    using Autofac.Integration.Mvc;
    using Doamin.Service.Authentication;
    using Doamin.Service.Factory;
    using Doamin.Service.Helpers;
    using Doamin.Service.Order;
    using Doamin.Service.Payments;
    using Doamin.Service.Products;
    using Doamin.Service.Security;
    using Doamin.Service.Stores;
    using Doamin.Service.Users;
    using Domain.EntityFramework;
    using EasyErp.Core;
    using EasyErp.Core.Configuration.Settings;
    using EasyErp.Core.Infrastructure;
    using EasyErp.Core.Infrastructure.DependencyManagement;
    using EasyERP.Web.Framework.Mvc.Routes;
    using EasyERP.Web.Framework.UI;
    using Infrastructure.Domain;
    using Infrastructure.Domain.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;

    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //HTTP context and other related stuff
            builder.Register(c => new HttpContextWrapper(HttpContext.Current) as HttpContextBase)
                   .As<HttpContextBase>()
                   .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                   .As<HttpRequestBase>()
                   .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                   .As<HttpResponseBase>()
                   .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                   .As<HttpServerUtilityBase>()
                   .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                   .As<HttpSessionStateBase>()
                   .InstancePerLifetimeScope();

            builder.RegisterInstance(new AreaSettings()).AsSelf();

            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();

            //user agent helper
            builder.RegisterType<UserAgentHelper>().As<IUserAgentHelper>().InstancePerLifetimeScope();

            //controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            //data layer
            const string ConnectionString = "easyerp_db";
            var dataContext = new EntityFrameworkDbContext(ConnectionString);

            //builder.Register<IEntityFrameworkDbContext>(c => dataContext).InstancePerLifetimeScope();
            builder.RegisterInstance(dataContext).As<IEntityFrameworkDbContext>();
            builder.Register<IUnitOfWork>(i => new EntityFrameworkUnitOfWork(dataContext))
                   .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EntityFrameworkRepository<>))
                   .As(typeof(IRepository<>))
                   .InstancePerLifetimeScope();

            //work context
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();

            // Services
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<InventoryService>().As<IInventoryService>().InstancePerLifetimeScope();
            builder.RegisterType<PaymentService>().As<IPaymentService>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeTimesheetService>().As<IEmployeeTimesheetService>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(DaliyStatisticService<>))
                   .As(typeof(IStatisticService<>))
                   .InstancePerLifetimeScope();
            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<EncryptionService>().As<IEncryptionService>().InstancePerLifetimeScope();
            builder.RegisterType<StandardPermissionProvider>().As<IPermissionProvider>().InstancePerLifetimeScope();
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductPriceService>().As<IProductPriceService>().InstancePerLifetimeScope();

            builder.RegisterType<PermissionService>().As<IPermissionService>().InstancePerLifetimeScope();
            builder.RegisterType<AclService>().As<IAclService>().InstancePerLifetimeScope();

            builder.RegisterType<StoreService>().As<IStoreService>().InstancePerLifetimeScope();

            builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();

            builder.RegisterType<DateTimeHelper>().As<IDateTimeHelper>().InstancePerLifetimeScope();
            builder.RegisterType<PageHeadBuilder>().As<IPageHeadBuilder>().InstancePerLifetimeScope();
            builder.RegisterType<RoutePublisher>().As<IRoutePublisher>().SingleInstance();
        }

        public int Order
        {
            get { return 0; }
        }
    }

    public class SettingsSource : IRegistrationSource
    {
        private static readonly MethodInfo BuildMethod = typeof(SettingsSource).GetMethod(
            "BuildRegistration",
            BindingFlags.Static | BindingFlags.NonPublic);

        public IEnumerable<IComponentRegistration> RegistrationsFor(
            Service service,
            Func<Service, IEnumerable<IComponentRegistration>> registrations)
        {
            var ts = service as TypedService;
            if (ts != null &&
                typeof(ISettings).IsAssignableFrom(ts.ServiceType))
            {
                var buildMethod = BuildMethod.MakeGenericMethod(ts.ServiceType);
                yield return (IComponentRegistration)buildMethod.Invoke(null, null);
            }
        }

        public bool IsAdapterForIndividualComponents
        {
            get { return false; }
        }
    }
}