namespace Domain.Model.Payments
{
    using System;
    using System.Collections.Generic;
    using Domain.Model.Orders;
    using Domain.Model.Products;
    using Infrastructure.Domain.Model;

    public class Payment : BaseEntity, IAggregateRoot
    {
        public Payment()
        {
            Items = new List<PayItem>();
        }

        public DateTime DueDateTime { get; set; }

        public double TotalAmount { get; set; }

        public int InventoryId { get; set; }

        public virtual Inventory Inventory { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public virtual ICollection<PayItem> Items { get; set; }
    }
}