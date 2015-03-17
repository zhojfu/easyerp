namespace Infrastructure.DependencyManagement
{
    using Autofac;

    public interface IDependencyRegistrar
    {
        int Order { get; }

        void Register(ContainerBuilder builder, ITypeFinder typeFinder);
    }
}