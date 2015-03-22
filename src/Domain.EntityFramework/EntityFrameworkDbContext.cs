namespace Domain.EntityFramework
{
    using System;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Reflection;
    using Domain.EntityFramework.Configurations;
    using Infrastructure.Domain.EntityFramework;
    using Infrastructure.Domain.Model;
    using System.Data.Entity;

    public class EntityFrameworkDbContext : DbContext, IEntityFrameworkDbContext
    {
        public EntityFrameworkDbContext(string connectionString)
            : base(connectionString)
        {
            //DbDatabase.SetInitialzer(null);
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class, IAggregateRoot
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                                          .Where(type => !String.IsNullOrEmpty(type.Namespace))
                                          .Where(
                                              type => type.BaseType != null && type.BaseType.IsGenericType &&
                                                      type.BaseType.GetGenericTypeDefinition() ==
                                                      typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            
            ////modelBuilder.Configurations.Add(new TestDoublesConfiguration());
            //modelBuilder.Configurations.Add(new ProductConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}