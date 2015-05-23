namespace Domain.Model.Customer
{
    using System;
    using System.Collections.Generic;
    using Domain.Model.Orders;
    using Infrastructure.Domain.Model;

    public class Customer : BaseEntity, IAggregateRoot
    {
        public override string Name { get; set; }

        public string Address { get; set; }

        public string TelePhone { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string IdNumber { get; set; }

        public bool Male { get; set; }

        public DateTime Birth { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}