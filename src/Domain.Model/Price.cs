namespace Domain.Model
{
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;

    public class Price : BaseEntity, IAggregateRoot
    {
        public double Cost { get; set; }

        public double SalePrice { get; set; } // define by comapny

        public double Discount { get; set; }

        public DateTime UpdataTime { get; set; }

        public Product Product { get; set; }

        public string ProductId { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}