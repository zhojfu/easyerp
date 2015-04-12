namespace Domain.EntityFramework.Configurations.Base
{
    using Domain.Model.Base;
    using System.Data.Entity.ModelConfiguration;

    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            this.ToTable("User");
            this.HasKey(u => u.Id);
            //this.Property(u => u.Name).IsRequired();
            this.Property(u => u.Password).IsRequired();
            this.Property(u => u.Active);
            this.HasRequired(u => u.Company).WithMany().HasForeignKey(u => u.CompanyId);
        }
    }
}