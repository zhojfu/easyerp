namespace Domain.EntityFramework.Configurations.Purchase
{
    using Domain.Model.Products;
    using Domain.Model.Purchase;
    using Infrastructure.Domain.Model;
    using System;
    using System.Data.Entity.ModelConfiguration;

    public class OrderLineConfiguration : EntityTypeConfiguration<OrderLine>
    {
        public OrderLineConfiguration()
        {
            this.ToTable("OrderLine");
            this.HasKey(ol => ol.Id);
            //this.Property(ol => ol.Name).IsRequired();
            this.Property(ol => ol.DatePlanned).IsRequired();
            this.HasRequired(ol => ol.Uom).WithMany().HasForeignKey(ol => ol.UomId);
            this.HasRequired(ol => ol.Product).WithMany().HasForeignKey(ol => ol.ProductId);
            this.Property(ol => ol.UintPrice);
            this.Property(ol => ol.SubtotalPrice);
            this.HasRequired(ol => ol.Order).WithMany().HasForeignKey(ol => ol.OrderId);
            this.Property(ol => ol.State);
            this.Property(ol => ol.Invoiced);
        }
    }

    public class OrderLine : BaseEntity
    {
        public enum Status
        {
            Draft,

            Confirmed,

            Done,

            Cancel
        }

        public float ProductQuantity { get; set; }

        public DateTime DatePlanned { get; set; }

        public long UomId { get; set; }

        public virtual ProductUom Uom { get; set; }

        public long ProductId { get; set; }

        public virtual Product Product { get; set; }

        public float UintPrice { get; set; }

        public float SubtotalPrice { get; set; }

        public long OrderId { get; set; }

        public virtual Order Order { get; set; }

        public Status State { get; set; }

        public bool Invoiced { get; set; }
    }
}