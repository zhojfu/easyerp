namespace Domain.Model.Products
{
    using System;
    using System.Collections.Generic;
    using Domain.Model.Factory;
    using Domain.Model.Stores;
    using Infrastructure.Domain.Model;

    public class ProductStoreMapping : BaseEntity, IAggregateRoot
    {
        public ProductStoreMapping()
        {
        }

        public int ProductId { get; set; }
        public int StoreId { get; set; }


        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
        public double Quantity { get; set; }

    }
}
