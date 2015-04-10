namespace Domain.Model.Products
{
    using Domain.Model.Company;
    using Infrastructure.Domain.Model;
    using System;

    public class ProductPriceHistory : BaseEntity
    {
        public DateTime DateTime { get; set; }

        public float Cost { get; set; }

        public long ProductTemplateId { get; set; }

        public virtual ProductTemplate ProductTemplate { get; set; }

        public long CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}