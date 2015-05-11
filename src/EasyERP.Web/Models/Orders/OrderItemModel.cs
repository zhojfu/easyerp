namespace EasyERP.Web.Models.Orders
{
    using EasyERP.Web.Framework.Mvc;
    using System;

    public class OrderItemModel : BaseEntityModel
    {
        public Guid OrderItemGuid { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public float Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal OriginalProductCost { get; set; }

        public string ProductName { get; set; }
    }
}