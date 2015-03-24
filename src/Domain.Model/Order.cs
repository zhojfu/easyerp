namespace Domain.Model
{
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;

    public class Order : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime ApplyDate { get; set; }

        public DateTime ApprovalTime { get; set; }

        public DateTime ReceiveTime { get; set; }

        public string Status { get; set; }

        public double Quantity { get; set; }

        public Store Store { get; set; }

        public Guid StoreId { get; set; }

        public Product Product { get; set; }

        public Price Price { get; set; }

        public Guid PriceId { get; set; }

        public ICollection<OrderComposition> CompositionProducts { get; set; }

        public ICollection<PayInfo> PayInfos { get; set; }
    }
}