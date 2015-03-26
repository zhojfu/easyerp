namespace EasyERP.Desktop.Product
{
    using EasyERP.Desktop.Price;
    using NullGuard;
    using PropertyChanged;
    using System;
    using System.Collections.Generic;

    [ImplementPropertyChanged]
    public class ProductModel
    {
        public ProductModel()
        {
            this.Prices = new List<PriceModel>();
        }

        public Guid Id { get; set; }

        [AllowNull]
        public string Upc { get; set; } //条形码 KEY

        [AllowNull]
        public string Name { get; set; }

        [AllowNull]
        public string Description { get; set; }

        [AllowNull]
        public string Unit { get; set; }

        [AllowNull]
        protected IList<PriceModel> Prices { get; set; }

        [AllowNull]
        public string Category { get; set; }

        public double? Price { get; set; }

        public class ProductCategoryModel
        {
            [AllowNull]
            public string Category { get; set; }

            public Guid ProductId { get; set; }

            public Guid CategoryId { get; set; }

            public bool IsFeaturedProduct { get; set; }

            public int DisplayOrder { get; set; }
        }
    }
}