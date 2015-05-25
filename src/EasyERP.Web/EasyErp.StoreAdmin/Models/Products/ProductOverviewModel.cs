namespace EasyErp.StoreAdmin.Models.Products
{
    using Infrastructure.Domain.Model;

    public class ProductOverviewModel : BaseEntity
    {
        public ProductOverviewModel()
        {
            ProductPrice = new ProductPriceModel();
        }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        //price
        public ProductPriceModel ProductPrice { get; set; }

        public class ProductPriceModel : BaseEntity
        {
            public decimal Price { get; set; }
        }
    }
}