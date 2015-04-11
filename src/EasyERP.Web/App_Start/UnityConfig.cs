using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace EasyERP.Web.App_Start
{
    using System.Data.Entity;
    using Doamin.Service.Factory;

    using Domain.EntityFramework;
    using Domain.Model.Factory;
    using Infrastructure.Domain;
    using Infrastructure.Domain.EntityFramework;
    using Microsoft.Ajax.Utilities;

    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            const string ConnectionString = "easyerp_db";
            var dbContext = new EntityFrameworkDbContext(ConnectionString);
            // container.RegisterInstance(typeof(DbContext),new EntityFrameworkDbContext(ConnectionString));
            container.RegisterInstance(typeof(IEntityFrameworkDbContext), dbContext);
            container.RegisterInstance(typeof(IUnitOfWork),  new EntityFrameworkUnitOfWork(dbContext));
            container.RegisterType(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));
            //container.RegisterType(typeof(IStatisticService<>), typeof(DaliyStatisticService<>));
            container.RegisterType(typeof(IEmployeeService), typeof(EmployeeService));
            container.RegisterType(typeof(IEmployeeTimesheetService), typeof(EmployeeTimesheetService));
            // container.RegisterType<IUnitOfWork, EntityFrameworkUnitOfWork>();
            // container.RegisterType<IRepository<>>()
        }
    }
}
