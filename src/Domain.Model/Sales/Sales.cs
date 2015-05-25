namespace Domain.Model.Sales
{
    using System;
    using System.Collections.Generic;
    using Infrastructure.Domain.Model;

    public class Sales : BaseEntity, IAggregateRoot
    {
        public DateTime CreatedOnUtc { get; set; }

        public decimal Total { get; set; }

        public virtual ICollection<SalesItem> SaleItems { get; set; }
    }
}