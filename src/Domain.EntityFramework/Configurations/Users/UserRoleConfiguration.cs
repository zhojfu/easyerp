namespace Domain.EntityFramework.Configurations.Users
{
    using Domain.Model.Users;
    using System.Data.Entity.ModelConfiguration;

    public class UserRoleConfiguration : EntityTypeConfiguration<UserRole>
    {
        public UserRoleConfiguration()
        {
            ToTable("UserRole");
            HasKey(u => u.Id);
            Property(u => u.Name).IsRequired();
        }
    }
}