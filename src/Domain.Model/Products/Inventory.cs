namespace Domain.Model.Products
{
    using Infrastructure.Domain.Model;
    using System;

    public class Inventory : BaseEntity, IAggregateRoot
    {
        public DateTime InStockTime { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}