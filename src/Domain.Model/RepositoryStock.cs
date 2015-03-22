namespace Domain.Model
{
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;

    public class RepositoryStock : BaseEntity, IAggregateRoot
    {
        public DateTime StockTime { get; set; }

        public DateTime ProductionTime { get; set; }

        public double Quantity { get; set; }

        public double Cost { get; set; }

        public string Origin { get; set; }

        public Product Product { get; set; }

        public string ProductId { get; set; }

        //public ICollection<OrderComposition> OrderCompositions { get; set; }
    }
}