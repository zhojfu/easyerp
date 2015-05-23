namespace EasyERP.Web.Models.Orders
{
    using EasyERP.Web.Framework.Mvc;

    public class CartItemModel : BaseEntityModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public float Quantity { get; set; }
    }
}