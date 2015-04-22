namespace EasyERP.Web.Framework
{
    using Autofac;
    using Autofac.Builder;
    using Autofac.Core;
    using Autofac.Integration.Mvc;
    using Doamin.Service.Directory;
    using Doamin.Service.Discounts;
    using Doamin.Service.Factory;
    using Doamin.Service.Helpers;
    using Doamin.Service.Payments;
    using Doamin.Service.Products;
    using Doamin.Service.Security;
    using Doamin.Service.Stores;
    using Doamin.Service.Vendors;
    using Domain.EntityFramework;
    using Domain.Model.Factory;
    using Domain.Model.Products;
    using EasyErp.Core;
    using EasyErp.Core.Caching;
    using EasyErp.Core.Configuration;
    using EasyErp.Core.Infrastructure.DependencyManagement;
    using EasyERP.Web.Framework.UI;
    using Infrastructure.Domain;
    using Infrastructure.Domain.EntityFramework;
    using Nop.Core.Infrastructure;
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

            builder.RegisterGeneric(typeof(EntityFrameworkRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //work context
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();

            builder.RegisterType<NullCache>().As<ICacheManager>().Named<ICacheManager>("nop_cache_static").SingleInstance();
            // Services
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<InventoryService>().As<IInventoryService>().InstancePerLifetimeScope();
            builder.RegisterType<PaymentService>().As<IPaymentService>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>().InstancePerLifetimeScope();
            builder.RegisterType<ConsumptionService>().As<IConsumptionService>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeTimesheetService>().As<ITimesheetService<WorkTimeStatistic>>().InstancePerLifetimeScope();
            builder.RegisterType<ConsumptionTimesheetService>().As<ITimesheetService<ConsumptionStatistic>>().InstancePerLifetimeScope();
            builder.RegisterType<ManufacturerService>().As<IManufacturerService>().InstancePerLifetimeScope();

            //builder.RegisterType<ProductAttributeFormatter>().As<IProductAttributeFormatter>().InstancePerLifetimeScope();
            //builder.RegisterType<ProductAttributeParser>().As<IProductAttributeParser>().InstancePerLifetimeScope();
            builder.RegisterType<ProductAttributeService>().As<IProductAttributeService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
            builder.RegisterType<SpecificationAttributeService>()
                   .As<ISpecificationAttributeService>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<ProductTemplateService>().As<IProductTemplateService>().InstancePerLifetimeScope();

            //builder.RegisterType<CategoryTemplateService>().As<ICategoryTemplateService>().InstancePerLifetimeScope();
            //builder.RegisterType<ManufacturerTemplateService>().As<IManufacturerTemplateService>().InstancePerLifetimeScope();

            //builder.RegisterType<AffiliateService>().As<IAffiliateService>().InstancePerLifetimeScope();
            builder.RegisterType<VendorService>().As<IVendorService>().InstancePerLifetimeScope();

            //builder.RegisterType<GenericAttributeService>().As<IGenericAttributeService>().InstancePerLifetimeScope();
            //builder.RegisterType<FulltextService>().As<IFulltextService>().InstancePerLifetimeScope();

            //pass MemoryCacheManager as cacheManager (cache settings between requests)
            builder.RegisterType<PermissionService>().As<IPermissionService>()
                   .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
                   .InstancePerLifetimeScope();
            ////pass MemoryCacheManager as cacheManager (cache settings between requests)
            builder.RegisterType<AclService>().As<IAclService>()
                   .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
                   .InstancePerLifetimeScope();
            ////pass MemoryCacheManager as cacheManager (cache settings between requests)

            builder.RegisterType<MeasureService>().As<IMeasureService>().InstancePerLifetimeScope();

            builder.RegisterType<StoreService>().As<IStoreService>().InstancePerLifetimeScope();
            ////pass MemoryCacheManager as cacheManager (cache settings between requests)
            //builder.RegisterType<StoreMappingService>().As<IStoreMappingService>()
            //    .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
            //    .InstancePerLifetimeScope();

            builder.RegisterType<DiscountService>().As<IDiscountService>().InstancePerLifetimeScope();

            //builder.RegisterType<DownloadService>().As<IDownloadService>().InstancePerLifetimeScope();
            //builder.RegisterType<PictureService>().As<IPictureService>().InstancePerLifetimeScope();

            //builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
            //builder.RegisterType<OrderReportService>().As<IOrderReportService>().InstancePerLifetimeScope();
            //builder.RegisterType<OrderProcessingService>().As<IOrderProcessingService>().InstancePerLifetimeScope();
            //builder.RegisterType<OrderTotalCalculationService>().As<IOrderTotalCalculationService>().InstancePerLifetimeScope();

            //builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();

            //builder.RegisterType<DefaultLogger>().As<ILogger>().InstancePerLifetimeScope();

            //builder.RegisterType<SqlFileInstallationService>().As<IInstallationService>().InstancePerLifetimeScope();

            builder.RegisterType<DateTimeHelper>().As<IDateTimeHelper>().InstancePerLifetimeScope();
            builder.RegisterType<PageHeadBuilder>().As<IPageHeadBuilder>().InstancePerLifetimeScope();

            //builder.RegisterType<RoutePublisher>().As<IRoutePublisher>().SingleInstance();
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