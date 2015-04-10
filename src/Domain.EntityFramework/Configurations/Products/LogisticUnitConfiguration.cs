namespace Domain.EntityFramework.Configurations.Products
{
    using Domain.Model.Products;
    using System.Data.Entity.ModelConfiguration;

    internal class LogisticUnitConfiguration : EntityTypeConfiguration<ProductLogisticUnit>
    {
        public LogisticUnitConfiguration()
        {
            this.ToTable("ProductLogisticUnit");
            this.HasKey(lu => lu.Id);
            this.Property(lu => lu.Type).IsRequired();
        }
    }
}