namespace Domain.EntityFramework.Configurations.Customer
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Customer;

    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            HasKey(s => s.Id);
        }
    }
}