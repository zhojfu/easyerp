namespace EasyErp.Core.Infrastructure
{
    using System;
    using EasyErp.Core.Configuration;
    using EasyErp.Core.Infrastructure.DependencyManagement;

    public interface IEngine
    {
        ContainerManager ContainerManager { get; }
        void Initialize(EasyErpConfig config);
        T Resolve<T>() where T : class;
        object Resolve(Type type);
        T[] ResolveAll<T>();
    }
}