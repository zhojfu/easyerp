namespace Domain.Model.Products.Price
{
    using Domain.Model.Company;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;

    public class ProductPriceList : BaseEntity
    {
        public string Active { get; set; }

        public long PriceListTypeId { get; set; }

        public virtual ProductPriceListType Type { get; set; }

        public long CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<ProductPriceListVersion> Version { get; set; }
    }
}