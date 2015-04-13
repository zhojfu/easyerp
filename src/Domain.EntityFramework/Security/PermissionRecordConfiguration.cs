namespace Domain.EntityFramework.Security
{
    using Domain.Model.Security;
    using System.Data.Entity.ModelConfiguration;

    public class PermissionRecordConfiguration : EntityTypeConfiguration<PermissionRecord>
    {
        public PermissionRecordConfiguration()
        {
            this.ToTable("PermissionRecord");
            this.HasKey(pr => pr.Id);
            this.Property(pr => pr.Name).IsRequired();
            this.Property(pr => pr.SystemName).IsRequired().HasMaxLength(255);
            this.Property(pr => pr.Category).IsRequired().HasMaxLength(255);

            this.HasMany(pr => pr.CustomerRoles)
                .WithMany(cr => cr.PermissionRecords)
                .Map(m => m.ToTable("PermissionRecord_Role_Mapping"));
        }
    }
}