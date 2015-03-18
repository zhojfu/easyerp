namespace EasyERP.Desktop
{
    using Autofac;
    using Infrastructure.DependencyManagement;

    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order
        {
            get { return 0; }
        }

        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
        }
    }
}