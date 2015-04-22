namespace Domain.EntityFramework.Configurations
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model;

    internal class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            HasKey(e => e.Id).ToTable(typeof(Employee).Name);
            Property(e => e.FirstName);
            Property(e => e.LastName);
            Property(e => e.IdNumber);
            Property(e => e.Male);
            Property(e => e.Married);
            Property(e => e.HomePhone);
            Property(e => e.Department);
            Property(e => e.NativePlace);
            Property(e => e.CellPhone);
            Property(e => e.Birth);
            Property(e => e.Education);
            Property(e => e.Address);
            Property(e => e.Race);
            Property(e => e.Email);
            Property(e => e.SalaryOfMonth);
            Property(e => e.Photo);
        }
    }
}