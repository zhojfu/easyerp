namespace Domain.Model.Products.Price
{
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;

    public class ProductPriceListVersion : BaseEntity
    {
        public long PriceListId { get; set; }

        public ProductPriceList PriceList { get; set; }

        public bool Active { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual ICollection<ProductPriceListItem> Items { get; set; }
    }
}