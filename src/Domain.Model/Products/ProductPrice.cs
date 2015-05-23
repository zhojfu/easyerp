namespace Domain.Model.Products
{
    using System;
    using Domain.Model.Stores;
    using Infrastructure.Domain.Model;

    public class ProductPrice : BaseEntity, IAggregateRoot
    {
        public DateTime DateTime { get; set; }

        public decimal Price { get; set; }

        public int StoreId { get; set; }

        public virtual Store Store { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}