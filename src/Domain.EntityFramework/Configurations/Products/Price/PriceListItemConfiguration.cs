namespace Domain.EntityFramework.Configurations.Products.Price
{
    using Domain.Model.Products.Price;
    using System.Data.Entity.ModelConfiguration;

    public class PriceListItemConfiguration : EntityTypeConfiguration<ProductPriceListItem>
    {
        public PriceListItemConfiguration()
        {
            this.ToTable("ProductPriceListItem");
            this.HasKey(i => i.Id);
            //this.Property(i => i.Name).IsRequired();
            this.HasRequired(i => i.PriceVersion)
                .WithMany()
                .HasForeignKey(i => i.PriceListVersionId)
                .WillCascadeOnDelete();
            this.HasRequired(i => i.ProductTemplate)
                .WithMany()
                .HasForeignKey(i => i.ProductTemplateId)
                .WillCascadeOnDelete();
            this.HasRequired(i => i.Product).WithMany().HasForeignKey(i => i.ProductId).WillCascadeOnDelete();
            this.HasRequired(i => i.Category).WithMany().HasForeignKey(i => i.CategoryId).WillCascadeOnDelete();
            this.Property(i => i.MinQuantity).IsRequired();
            this.Property(i => i.Order);
            this.HasOptional(i => i.PriceList).WithMany().HasForeignKey(i => i.PriceListId);
            this.Property(i => i.Surcharge);
            this.Property(i => i.Discount);
            this.Property(i => i.Round);
            this.Property(i => i.MinMargin);
            this.Property(i => i.MaxMargin);
        }
    }
}