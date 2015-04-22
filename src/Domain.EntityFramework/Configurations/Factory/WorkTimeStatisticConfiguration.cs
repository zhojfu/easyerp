namespace Domain.EntityFramework.Configurations.Factory
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Factory;

    internal class WorkTimeStatisticConfiguration : EntityTypeConfiguration<WorkTimeStatistic>
    {
        public WorkTimeStatisticConfiguration()
        {
            HasKey(w => w.Id).ToTable("WorkTimeRecord");
            Property(w => w.SalaryOfDay);
            Property(w => w.WorkTimeHr);
            Property(w => w.Date);
            HasRequired(w => w.Employee).WithMany(wk => wk.WorkTimeRecords).HasForeignKey(w => w.EmployeeId);
        }
    }
}