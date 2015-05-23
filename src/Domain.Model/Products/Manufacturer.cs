namespace Domain.Model.Products
{
    using System;
    using System.Collections.Generic;
    using Infrastructure.Domain.Model;

    public class Manufacturer : BaseEntity
    {
        public string Description { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime UpdatedOnUtc { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}