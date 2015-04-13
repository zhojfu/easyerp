namespace Domain.Model.Stores
{
    using Domain.Model.Company;
    using Domain.Model.Orders;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;

    public partial class Store : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public int DisplayOrder { get; set; }

        public string StoreName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}