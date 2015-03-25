namespace Domain.EntityFramework.Configurations
{
    using Domain.Model;
    using System.Data.Entity.ModelConfiguration;
    using System.Runtime.CompilerServices;

    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            this.ToTable("Category");
            this.HasKey(c => c.Id);
        }
    }
}