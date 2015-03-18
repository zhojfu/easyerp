namespace EasyERP.Desktop
{
    using Autofac;
    using Doamin.Service;
    using Domain.EntityFramework;
    using Domain.Model;
    using Infrastructure.Domain;
    using Infrastructure.Domain.Data;
    using Infrastructure.Domain.EntityFramework;

    public class DomainServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //data layer
            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();
            builder.Register(c => dataSettingsManager.LoadSettings()).As<DataSettings>();
            builder.Register(x => new EfDataProviderManager(x.Resolve<DataSettings>()))
                   .As<BaseDataProviderManager>()
                   .InstancePerDependency();

            builder.Register(x => x.Resolve<BaseDataProviderManager>().LoadDataProvider())
                   .As<IDataProvider>()
                   .InstancePerDependency();

            if (dataProviderSettings != null &&
                dataProviderSettings.IsValid())
            {
                var efDataProviderManager = new EfDataProviderManager(dataSettingsManager.LoadSettings());
                var dataProvider = efDataProviderManager.LoadDataProvider();
                dataProvider.InitConnectionFactory();
                var dataContext = new EntityFrameworkDbContext(dataProviderSettings.DataConnectionString);
                builder.Register<IEntityFrameworkDbContext>(c => dataContext)
                       .InstancePerLifetimeScope();
                builder.Register<IUnitOfWork>(i => new EntityFrameworkUnitOfWork(dataContext))
                       .InstancePerLifetimeScope();
            }
            else
            {
                var dataContext = new EntityFrameworkDbContext(dataSettingsManager.LoadSettings().DataConnectionString);
                builder.Register<IEntityFrameworkDbContext>(c => dataContext).InstancePerLifetimeScope();
                builder.Register<IUnitOfWork>(i => new EntityFrameworkUnitOfWork(dataContext))
                       .InstancePerLifetimeScope();
            }

            builder.RegisterGeneric(typeof(EntityFrameworkRepository<>))
                   .As(typeof(IRepository<>))
                   .InstancePerLifetimeScope();

            //builder.RegisterType<EntityFrameworkUnitOfWork>().As<IUnitOfWork>();

            builder.RegisterType<Product>().AsSelf();
            builder.RegisterType<ProductService>().AsSelf();
        }
    }
}