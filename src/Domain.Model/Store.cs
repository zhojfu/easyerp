namespace Domain.Model
{
    using Infrastructure.Domain.Model;
    using System.Collections.Generic;

    public class Store : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}