namespace EasyERP.Web.Models.Orders
{
    using EasyERP.Web.Framework.Mvc;

    public class SearchModel : BaseModel
    {
        public int OrderStatus { get; set; }

        public int PayStatus { get; set; }

        public int StoreId { get; set; }
    }
}