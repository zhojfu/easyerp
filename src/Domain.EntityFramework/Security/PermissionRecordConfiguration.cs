namespace Domain.EntityFramework.Security
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Security;

    public class PermissionRecordConfiguration : EntityTypeConfiguration<PermissionRecord>
    {
        public PermissionRecordConfiguration()
        {
            ToTable("PermissionRecord");
            HasKey(pr => pr.Id);
            Property(pr => pr.Name).IsRequired();
            Property(pr => pr.SystemName).IsRequired().HasMaxLength(255);
            Property(pr => pr.Category).IsRequired().HasMaxLength(255);

            HasMany(pr => pr.CustomerRoles)
                .WithMany(cr => cr.PermissionRecords)
                .Map(m => m.ToTable("PermissionRecord_Role_Mapping"));
        }
    }
}