namespace Domain.Model.Products
{
    using Infrastructure.Domain.Model;
    using System;

    public class Category : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ItemNo { get; set; }

        public bool Published { get; set; }

        public bool Deleted { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime UpdatedOnUtc { get; set; }
    }
}