namespace Domain.Model
{
    using Infrastructure.Domain.Model;
    using System;

    public class RepositoryStock : BaseEntity, IAggregateRoot
    {
        public DateTime StockTime { get; set; }

        public DateTime ProductionTime { get; set; }

        public double Quantity { get; set; }

        public double Cost { get; set; }

        public string Origin { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }
    }
}