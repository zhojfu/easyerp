namespace Domain.Model.Orders
{
    using System;
    using Domain.Model.Products;
    using Infrastructure.Domain.Model;

    public class OrderItem : BaseEntity, IAggregateRoot
    {
        public Guid OrderItemGuid { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public float Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal OriginalProductCost { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}