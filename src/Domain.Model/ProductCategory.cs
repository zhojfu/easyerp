namespace Domain.Model
{
    using Infrastructure.Domain.Model;
    using System;

    public class ProductCategory : BaseEntity, IAggregateRoot
    {
        public string ProductId { get; set; }

        public Guid CategoryId { get; set; }

        public bool IsFeaturedProduct { get; set; }

        public int DisplayOrder { get; set; }

        public virtual Category Category { get; set; }

        public virtual Product Product { get; set; }
    }
}