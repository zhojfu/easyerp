namespace Domain.EntityFramework.Configurations
{
    using Domain.Model;
    using System.Data.Entity.ModelConfiguration;

    public class RepositoryStockConfiguration : EntityTypeConfiguration<RepositoryStock>
    {
        public RepositoryStockConfiguration()
        {
            this.HasKey(r => r.Id).ToTable("Stock");
            this.Property(r => r.StockTime).HasColumnType("datetime2");
            this.Property(r => r.ProductionTime).HasColumnType("datetime2");
            this.Property(r => r.Quantity);
            this.Property(r => r.Cost);
            this.Property(r => r.Origin);

            this.HasRequired(r => r.Product)
                .WithMany(p => p.RepositoryStocks)
                .HasForeignKey(r => r.ProductId)
                .WillCascadeOnDelete(false);
        }
    }
}