namespace Domain.Model.Sales
{
    using Domain.Model.Factory;
    using Domain.Model.Products;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;

    public class SalesItem : BaseEntity, IAggregateRoot
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}