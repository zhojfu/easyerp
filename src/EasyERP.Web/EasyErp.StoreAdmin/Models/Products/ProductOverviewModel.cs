namespace EasyErp.StoreAdmin.Models.Products
{
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;

    public partial class ProductOverviewModel : BaseEntity
    {
        public ProductOverviewModel()
        {
            this.ProductPrice = new ProductPriceModel();
        }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        //price
        public ProductPriceModel ProductPrice { get; set; }

        public partial class ProductPriceModel : BaseEntity
        {
            public string Price { get; set; }
        }
    }
}