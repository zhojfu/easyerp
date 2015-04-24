namespace Domain.Model.Sales
{
    using Domain.Model.Factory;
    using Domain.Model.Products;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;

    public class Sales : BaseEntity, IAggregateRoot
    {
        public DateTime CreatedOnUtc { get; set; }

        public decimal Total { get; set; }

        public virtual ICollection<SalesItem> SaleItems { get; set; }
    }
}