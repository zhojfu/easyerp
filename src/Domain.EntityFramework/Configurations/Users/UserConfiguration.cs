namespace Domain.EntityFramework.Configurations.Users
{
    using Domain.Model.Users;
    using System.Data.Entity.ModelConfiguration;

    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("User");
            HasKey(u => u.Id);
            Property(u => u.Name);
            Property(u => u.Password).IsRequired();
            Property(u => u.Active);
            HasRequired(u => u.Store).WithMany().HasForeignKey(u => u.StoreId);
            HasMany(u => u.UserRoles).WithMany().Map(m => m.ToTable("User_UserRole_Mapping"));
        }
    }
}