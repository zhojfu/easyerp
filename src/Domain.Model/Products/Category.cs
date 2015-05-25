namespace Domain.Model.Products
{
    using System;
    using Infrastructure.Domain.Model;

    public class Category : BaseEntity, IAggregateRoot
    {
        public string Description { get; set; }

        public string ItemNo { get; set; }

        public bool Published { get; set; }

        public bool Deleted { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime UpdatedOnUtc { get; set; }
    }
}