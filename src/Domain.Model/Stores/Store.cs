namespace Domain.Model.Stores
{
    using Infrastructure.Domain.Model;
    using System;

    /// <summary>
    /// Represents a store
    /// </summary>
    public partial class Store : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public int DisplayOrder { get; set; }

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public string CompanyPhoneNumber { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}