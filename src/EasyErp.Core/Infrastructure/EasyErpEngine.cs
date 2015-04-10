namespace EasyErp.Core.Infrastructure
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using EasyErp.Core.Configuration;
    using EasyErp.Core.Infrastructure.DependencyManagement;
    using Nop.Core.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class EasyErpEngine : IEngine
    {
        #region Utilities

        protected virtual void RunStartupTasks()
        {
            var typeFinder = this.ContainerManager.Resolve<ITypeFinder>();
            var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
            var startUpTasks = new List<IStartupTask>();
            foreach (var startUpTaskType in startUpTaskTypes)
            {
                startUpTasks.Add((IStartupTask)Activator.CreateInstance(startUpTaskType));
            }

            //sort
            startUpTasks = startUpTasks.AsQueryable().OrderBy(st => st.Order).ToList();
            foreach (var startUpTask in startUpTasks)
                startUpTask.Execute();
        }

        protected virtual void RegisterDependencies(EasyErpConfig config)
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            //dependencies
            var typeFinder = new WebAppTypeFinder(config);
            builder = new ContainerBuilder();
            builder.RegisterInstance(config).As<EasyErpConfig>().SingleInstance();
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();
            builder.Update(container);

            //register dependencies provided by other assemblies
            builder = new ContainerBuilder();
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var drInstances = new List<IDependencyRegistrar>();
            foreach (var drType in drTypes)
            {
                drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
            }

            //sort
            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(builder, typeFinder);
            builder.Update(container);

            this.ContainerManager = new ContainerManager(container);

            //set dependency resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        #endregion Utilities

        public void Initialize(EasyErpConfig config)
        {
            this.RegisterDependencies(config);

            if (!config.IgnoreStartupTasks)
            {
                this.RunStartupTasks();
            }
        }

        public T Resolve<T>() where T : class
        {
            return this.ContainerManager.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return this.ContainerManager.Resolve(type);
        }

        public T[] ResolveAll<T>()
        {
            return this.ContainerManager.ResolveAll<T>();
        }

        public ContainerManager ContainerManager { get; private set; }
    }
}