namespace Domain.Model
{
    using Infrastructure.Domain.Model;
    using System;

    public class OrderComposition : BaseEntity, IAggregateRoot
    {
        public double Quantity { get; set; }

        public RepositoryStock Stock { get; set; }

        public Guid StockId { get; set; }

        public Order Order { get; set; }

        public Guid OrderId { get; set; }
    }
}