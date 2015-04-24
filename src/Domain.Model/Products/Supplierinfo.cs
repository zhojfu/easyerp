namespace Domain.Model.Products
{
    using Domain.Model.Company;
    using Infrastructure.Domain.Model;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class Supplierinfo : BaseEntity
    {
        public string ProductName { get; set; }

        public string ProductCode { get; set; }

        [DefaultValue(1)]
        public int Order { get; set; }

        [DefaultValue(0.0)]
        public float MinQuantity { get; set; }

        public long ProductTemplateId { get; set; }

        [DefaultValue(1)]
        public int Delay { get; set; }

        public long CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}