namespace Domain.Model.Products
{
    using Infrastructure.Domain.Model;
    using System;

    public class ProductPriceListPartnerinfo : BaseEntity
    {
        public float MinQuantity { get; set; }

        public float Price { get; set; }

        public long SupplierInfoId { get; set; }

        public virtual Supplierinfo Supplierinfo { get; set; }
    }
}