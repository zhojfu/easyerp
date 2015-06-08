
namespace Domain.Model.Products
{

    using Domain.Model.Stores;

    public class StockModel
    {
        public float Quantity { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }

    }
}