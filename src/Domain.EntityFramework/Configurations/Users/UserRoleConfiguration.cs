namespace Domain.EntityFramework.Configurations.Users
{
    using Domain.Model.Users;
    using System.Data.Entity.ModelConfiguration;

    public class UserRoleConfiguration : EntityTypeConfiguration<UserRole>
    {
        public UserRoleConfiguration()
        {
            this.ToTable("UserRole");
            this.HasKey(u => u.Id);
            this.Property(u => u.Name).IsRequired();
        }
    }
}