namespace Domain.Model.Products
{
    using Domain.Model.Company;
    using Domain.Model.Stores;
    using Infrastructure.Domain.Model;
    using System;

    public class ProductPriceHistory : BaseEntity
    {
        public DateTime DateTime { get; set; }

        public float Cost { get; set; }

        public int StoreId { get; set; }

        public virtual Store Store { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}