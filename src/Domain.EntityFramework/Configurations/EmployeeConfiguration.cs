
namespace Domain.EntityFramework.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using Domain.Model;

    internal class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            this.HasKey(e => e.Id).ToTable(typeof(Employee).Name);
            this.Property(e => e.FirstName);
            this.Property(e => e.LastName);
            this.Property(e => e.IdNumber);
            this.Property(e => e.Male);
            this.Property(e => e.Married);
            this.Property(e => e.HomePhone);
            this.Property(e => e.Department);
            this.Property(e => e.NativePlace);
            this.Property(e => e.CellPhone);
            this.Property(e => e.Birth);
            this.Property(e => e.EduBackground);
            this.Property(e => e.Address);
            this.Property(e => e.Race);
            this.Property(e => e.Zip);
            this.Property(e => e.SalaryOfMonth);
            this.Property(e => e.Photo);
        }
    }
}
