namespace Domain.Model.Products
{
    using Domain.Model.Company;
    using Infrastructure.Domain.Model;
    using System;

    public class ProductPriceHistory : BaseEntity
    {
        public DateTime DateTime { get; set; }

        public float Cost { get; set; }

        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}