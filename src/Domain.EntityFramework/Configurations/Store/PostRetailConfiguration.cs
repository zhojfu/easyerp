namespace Domain.EntityFramework.Configurations.Store
{
    using System.Data.Entity.ModelConfiguration;
    using Model.Stores;

    public class PostRetailConfiguration : EntityTypeConfiguration<PostRetail>
    {
        public PostRetailConfiguration()
        {
            HasKey(s => s.Id);
            HasRequired(p => p.Product).WithMany().HasForeignKey(p => p.ProductId);
        }
    }
}