namespace EasyERP.Desktop
{
    using Autofac;
    using Caliburn.Micro;
    using EasyERP.Desktop.Contacts;
    using EasyERP.Desktop.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using IContainer = Autofac.IContainer;

    public class AutofacBootstrapper : BootstrapperBase
    {
        public AutofacBootstrapper()
        {
            this.Initialize();
        }

        protected override void Configure()
        {
            this.ConfigureBootstrapper();

            if (this.CreateWindowManager == null)
            {
                throw new ArgumentNullException(@"CreateWindowManager");
            }
            if (this.CreateEventAggregator == null)
            {
                throw new ArgumentNullException(@"CreateEventAggregator");
            }

            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                   .Where(type => type.Name.EndsWith("ViewModel"))
                   .Where(
                       type =>
                       !this.EnforceNamespaceConvention ||
                       (!(string.IsNullOrWhiteSpace(type.Namespace)) && type.Namespace.EndsWith("ViewModels")))
                   .Where(type => type.GetInterface(this.ViewModelBaseType.Name, false) != null)
                   .AsSelf()
                   .InstancePerDependency();

            //  register views
            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                   .Where(type => type.Name.EndsWith("View"))
                   .Where(
                       type =>
                       !this.EnforceNamespaceConvention ||
                       (!(string.IsNullOrWhiteSpace(type.Namespace)) && type.Namespace.EndsWith("Views")))
                   .AsSelf()
                   .InstancePerDependency();

            builder.Register(c => this.CreateWindowManager()).InstancePerLifetimeScope();
            builder.Register(c => this.CreateEventAggregator()).InstancePerLifetimeScope();

            //  should we install the auto-subscribe event aggregation handler module?
            if (this.AutoSubscribeEventAggegatorHandlers)
            {
                builder.RegisterModule<EventAggregationAutoSubscriptionModule>();
            }

            //  allow derived classes to add to the container
            this.ConfigureContainer(builder);

            this.Container = builder.Build();
        }

        protected void RunStartupTasks()
        {
            //var typeFinder = this.Container.Resolve<ITypeFinder>();
            //var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
            //var startUpTasks = new List<IStartupTask>();
            //foreach (var startUpTaskType in startUpTaskTypes)
            //{
            //    startUpTasks.Add((IStartupTask)Activator.CreateInstance(startUpTaskType));
            //}

            ////sort
            //startUpTasks = startUpTasks.AsQueryable().OrderBy(st => st.Order).ToList();
            //foreach (var startUpTask in startUpTasks)
            //{
            //    startUpTask.Execute();
            //}
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            ////ensure database is installed
            //if (!DataSettingsHelper.DatabaseIsInstalled())
            //{
            //}

            //EngineContext.Initialize(false);
            this.RunStartupTasks();
            this.DisplayRootViewFor<IShell>();
        }

        protected override object GetInstance(Type service, string key)
        {
            object instance;
            if (string.IsNullOrWhiteSpace(key))
            {
                if (this.Container.TryResolve(service, out instance))
                {
                    return instance;
                }
            }
            else
            {
                if (this.Container.TryResolveNamed(key, service, out instance))
                {
                    return instance;
                }
            }
            throw new Exception(string.Format("Could not locate any instances of contract {0}.", key ?? service.Name));
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return this.Container.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance)
        {
            this.Container.InjectProperties(instance);
        }

        protected virtual void ConfigureBootstrapper()
        {
            this.EnforceNamespaceConvention = true;

            this.AutoSubscribeEventAggegatorHandlers = false;

            this.ViewModelBaseType = typeof(INotifyPropertyChanged);

            this.CreateWindowManager = () => new WindowManager();

            this.CreateEventAggregator = () => new EventAggregator();

            this.EnforceNamespaceConvention = false;

            this.AutoSubscribeEventAggegatorHandlers = true;
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            //var path = Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location);

            return new[]
            {
                Assembly.GetExecutingAssembly()
            };
        }

        protected virtual void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<DomainServicesModule>();
            builder.RegisterModule<AutofacDesktopModule>();
            //var typeFinder = new DesktopTypeFinder();

            //builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();

            ////register dependencies provided by other assemblies
            //var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            //var drInstances = drTypes.Select(drType => (IDependencyRegistrar)Activator.CreateInstance(drType)).ToList();

            ////sort
            //drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            //foreach (var dependencyRegistrar in drInstances)
            //{
            //    dependencyRegistrar.Register(builder, typeFinder);
            //}
        }

        #region Properties

        protected IContainer Container { get; private set; }

        public bool EnforceNamespaceConvention { get; set; }

        public bool AutoSubscribeEventAggegatorHandlers { get; set; }

        public Type ViewModelBaseType { get; set; }

        public Func<IWindowManager> CreateWindowManager { get; set; }

        public Func<IEventAggregator> CreateEventAggregator { get; set; }

        #endregion Properties
    }
}