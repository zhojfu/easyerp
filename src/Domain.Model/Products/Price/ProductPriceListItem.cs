namespace Domain.Model.Products.Price
{
    using Infrastructure.Domain.Model;
    using System;

    public class ProductPriceListItem : BaseEntity
    {
        public long PriceListVersionId { get; set; }

        public virtual ProductPriceListVersion PriceVersion { get; set; }

        public long ProductTemplateId { get; set; }

        public virtual ProductTemplate ProductTemplate { get; set; }

        public long ProductId { get; set; }

        public virtual Product Product { get; set; }

        public long CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int MinQuantity { get; set; }

        public int Order { get; set; }

        public long PriceListId { get; set; }

        public virtual ProductPriceList PriceList { get; set; }

        public float Surcharge { get; set; }

        public float Discount { get; set; }

        public float Round { get; set; }

        public float MinMargin { get; set; }

        public float MaxMargin { get; set; }
    }
}