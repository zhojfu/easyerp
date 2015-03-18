namespace EasyERP.Desktop
{
    using Autofac;
    using EasyERP.Desktop.Contacts;
    using EasyERP.Desktop.ViewModels;

    public class AutofacDesktopModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<ViewModelMetadataAttribute>().AsSelf();
            builder.RegisterType<ProductViewModel>().As<IViewModel>().WithMetadata("Order", 100);
            builder.RegisterType<ShellViewModel>().AsSelf().As<IShell>().SingleInstance();
        }
    }
}