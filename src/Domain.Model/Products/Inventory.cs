using Domain.Model.Stores;

namespace Domain.Model.Products
{
    using System;
    using Domain.Model.Payments;
    using Infrastructure.Domain.Model;

    public class Inventory : BaseEntity, IAggregateRoot
    {
        public DateTime InStockTime { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }

        public int PaymentId { get; set; }

        public virtual Payment Payment { get; set; }
    }
}