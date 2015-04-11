namespace Domain.Model.Payment
{
    using Domain.Model.Products;
    using Infrastructure.Domain.Model;
    using System;

    public class Payment : BaseEntity, IAggregateRoot
    {
        public DateTime DueDateTime { get; set; }

        public float Payables { get; set; }

        public float Paid { get; set; }

        public int InventoryId { get; set; }

        public Inventory Inventory { get; set; }
    }
}