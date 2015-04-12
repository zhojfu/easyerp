namespace Domain.Model.Products
{
    using Infrastructure.Domain.Model;
    using System;
    using System.ComponentModel;

    public class ProductPackaging : BaseEntity
    {
        [DefaultValue(1)]
        public int Order { get; set; }

        public float Quantity { get; set; }

        public long LogisticUnitId { get; set; }

        public virtual ProductLogisticUnit LogisticUnit { get; set; }

        public int LogisticUnitQuantity { get; set; }

        public long ContainerLogisticUnitId { get; set; }

        public virtual ProductLogisticUnit ContainerLogisticUnit { get; set; }

        [DefaultValue(3)]
        public int Rows { get; set; }

        public long ProductTemplateId { get; set; }

        public virtual ProductTemplate ProductTemplate { get; set; }

        public string EanCode { get; set; }

        public string Code { get; set; }

        public float Weight { get; set; }
    }
}