namespace EasyERP.Web.Models.Products
{
    using Domain.Model.Products;
    using Domain.Model.Stores;
    using EasyERP.Web.Framework.Mvc;
    using System;

    public class PriceModel : BaseModel
    {
        public DateTime DateTime { get; set; }

        public decimal Cost { get; set; }

        public decimal Price { get; set; }

        public int StoreId { get; set; }

        public int ProductId { get; set; }

        public string StoreName { get; set; }

        public string ProductName { get; set; }
    }
}