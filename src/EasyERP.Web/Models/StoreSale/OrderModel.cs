namespace EasyERP.Web.Models.StoreSale
{
    using System.Collections.Generic;

    public class OrderModel
    {
        public int CustomerId { get; set; }

        public string Title { get; set; }

        public List<OrderItemModel> OrderItems { get; set; }
    }

    public class OrderItemModel
    {
        public string Name { get; set; }

        public int ProductId { get; set; }

        public decimal PriceOfUnit { get; set; }

        public float Quantity { get; set; }
    }
}