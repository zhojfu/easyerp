using System.Data.Entity.ModelConfiguration;

namespace EasyERP.Domain.Persistence.EntityFramework.Mapping
{
    public abstract class NopEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class
    {
        protected NopEntityTypeConfiguration()
        {
            PostInitialize();
        }

        /// <summary>
        /// Developers can override this method in custom partial classes
        /// in order to add some custom initialization code to constructors
        /// </summary>
        protected virtual void PostInitialize()
        {
            
        }
    }
}