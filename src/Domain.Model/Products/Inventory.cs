namespace Domain.Model.Products
{
    using System;
    using Infrastructure.Domain.Model;

    public class Inventory : BaseEntity, IAggregateRoot
    {
        public DateTime InStockTime { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}