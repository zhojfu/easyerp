namespace Domain.Model
{
    using Infrastructure.Domain.Model;
    using System;

    public class Category : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }

        public string Descriiption { get; set; }

        public Guid ParentCategoryId { get; set; }
    }
}