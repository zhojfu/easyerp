namespace EasyERP.Web.Models.Products
{
    using EasyERP.Web.Framework.Mvc;

    public class PriceModel : BaseModel
    {
        public decimal CostPrice { get; set; }

        public decimal SalePrice { get; set; }

        public int StoreId { get; set; }

        public int ProductId { get; set; }

        public string StoreName { get; set; }

        public string ProductName { get; set; }
    }
}