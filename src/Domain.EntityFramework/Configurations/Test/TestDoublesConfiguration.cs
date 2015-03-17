using System.Data.Entity.ModelConfiguration;
using Domain.Model;

namespace Domain.EntityFramework.Configurations
{
    class TestDoublesConfiguration : EntityTypeConfiguration<TestDoubles>
    {
        public TestDoublesConfiguration()
        {
            HasKey(t => t.Id).ToTable("TestDoubles");
            Property(t => t.Name).HasMaxLength(20);
        }
    }
}
