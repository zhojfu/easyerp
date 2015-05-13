namespace EasyERP.Web.Models.StoreSale
{
    public class OrderModel
    {
    }

    public class OrderItemModel
    {
        public string Name { get; set; }
        public decimal PriceOfUnit { get; set; }
        public double Quantity { get; set; }
    }
}