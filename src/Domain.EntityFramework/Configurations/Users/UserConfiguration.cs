namespace Domain.EntityFramework.Configurations.Users
{
    using Domain.Model.Users;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            this.ToTable("User");
            this.HasKey(u => u.Id);
            this.Property(u => u.Name);
            this.Property(u => u.Password).IsRequired();
            this.Property(u => u.Active);
            this.HasRequired(u => u.Store).WithMany().HasForeignKey(u => u.StoreId);
            this.HasMany(u => u.UserRoles).WithMany().Map(m => m.ToTable("User_UserRole_Mapping"));
        }
    }
}