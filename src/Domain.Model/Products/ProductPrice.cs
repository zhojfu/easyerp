namespace Domain.Model.Products
{
    using Domain.Model.Stores;
    using Infrastructure.Domain.Model;
    using System;

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