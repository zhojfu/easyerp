namespace EasyERP.Web.Models.Orders
{
    using EasyERP.Web.Framework.Mvc;

    public class OrderItemModel : BaseEntityModel
    {
        public float Quantity { get; set; }

        public decimal Price { get; set; }

        public string ProductName { get; set; }
    }
}