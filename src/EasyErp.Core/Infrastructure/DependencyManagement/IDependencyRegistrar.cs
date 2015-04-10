namespace EasyErp.Core.Infrastructure.DependencyManagement
{
    using Autofac;
    using Nop.Core.Infrastructure;

    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder);

        int Order { get; }
    }
}