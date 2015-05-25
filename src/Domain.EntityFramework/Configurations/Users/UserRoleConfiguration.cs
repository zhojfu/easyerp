namespace Domain.EntityFramework.Configurations.Users
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Users;

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