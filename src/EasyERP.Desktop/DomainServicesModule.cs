namespace EasyERP.Desktop
{
    using Autofac;
    using Doamin.Service;
    using Domain.EntityFramework;
    using Infrastructure.Domain;
    using Infrastructure.Domain.EntityFramework;

    public class DomainServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            const string ConnectionString = "easyerp_db";
            var dataContext = new EntityFrameworkDbContext(ConnectionString);

            builder.Register<IEntityFrameworkDbContext>(c => dataContext).InstancePerLifetimeScope();
            builder.Register<IUnitOfWork>(i => new EntityFrameworkUnitOfWork(dataContext))
                   .InstancePerLifetimeScope();

            //            builder.RegisterType<CodeFirstInstallationService>().As<IInstallationService>();
            builder.RegisterGeneric(typeof(EntityFrameworkRepository<>))
                   .As(typeof(IRepository<>))
                   .InstancePerLifetimeScope();

            builder.RegisterType<Domain.Model.Product>().AsSelf();
            builder.RegisterType<ProductService>().AsSelf();
            builder.RegisterType<StockService>().AsSelf();
        }
    }
}