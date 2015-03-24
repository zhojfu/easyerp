namespace EasyERP.Desktop
{
    using Autofac;
    using EasyERP.Desktop.Contacts;
    using EasyERP.Desktop.Product;
    using EasyERP.Desktop.Stock;
    using EasyERP.Desktop.ViewModels;

    public class AutofacDesktopModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<ViewModelMetadataAttribute>().AsSelf();
            builder.RegisterType<ProductListViewModel>().As<IViewModel>().WithMetadata("Order", 100);
            builder.RegisterType<ListOrdersViewModel>().As<IViewModel>().WithMetadata("Order", 200);
            builder.RegisterType<StockManagerViewModel>().As<IViewModel>().WithMetadata("Order", 300);
            builder.RegisterType<ShellViewModel>().AsSelf().As<IShell>().SingleInstance();
        }
    }
}