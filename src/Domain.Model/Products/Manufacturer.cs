namespace Domain.Model.Products
{
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;

    public class Manufacturer : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime UpdatedOnUtc { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}