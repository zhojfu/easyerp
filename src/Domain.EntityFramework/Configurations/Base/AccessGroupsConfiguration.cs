namespace Domain.EntityFramework.Configurations.Base
{
    using Domain.Model.Base;
    using System.Data.Entity.ModelConfiguration;

    public class AccessGroupsConfiguration : EntityTypeConfiguration<AccessGroups>
    {
        public AccessGroupsConfiguration()
        {
            this.ToTable("AccessGroups");
            this.HasKey(ag => ag.Id);
            this.Property(ag => ag.Name).IsRequired();
            this.Property(ag => ag.Comment);
        }
    }
}