namespace Domain.Model.Company
{
    using Domain.Model.Stores;
    using Infrastructure.Domain.Model;
    using System.Collections;
    using System.Collections.Generic;

    public class Company : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public virtual ICollection<Store> Stores { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}