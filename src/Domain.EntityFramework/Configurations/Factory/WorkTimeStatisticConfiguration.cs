
namespace Domain.EntityFramework.Configurations.Factory
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Factory;

    internal class WorkTimeStatisticConfiguration : EntityTypeConfiguration<WorkTimeStatistic>
    {
        public WorkTimeStatisticConfiguration()
        {
            this.HasKey(w => w.Id).ToTable("WorkTimeRecord");
            this.Property(w => w.SalaryOfDay);
            this.Property(w => w.WorkTimeHr);
            this.Property(w => w.Date);
            this.HasRequired(w => w.Employee).WithMany(wk => wk.WorkTimeRecords).HasForeignKey(w => w.Id);
        }
    }
}
