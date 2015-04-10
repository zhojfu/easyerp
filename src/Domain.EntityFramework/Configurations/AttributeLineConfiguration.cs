namespace Domain.EntityFramework.Configurations
{
    using Domain.Model.Products;
    using System.Data.Entity.ModelConfiguration;

    public class AttributeLineConfiguration : EntityTypeConfiguration<AttributeLine>
    {
        public AttributeLineConfiguration()
        {
            this.ToTable("ProductAttributeLine");
            this.HasKey(al => al.Id);
            this.HasRequired(al => al.ProductTemplate)
                .WithMany()
                .HasForeignKey(al => al.ProductTemplateId)
                .WillCascadeOnDelete();
            this.HasRequired(al => al.Attribute).WithMany().HasForeignKey(al => al.AttributeId).WillCascadeOnDelete();
        }
    }
}