namespace Doamin.Service.Products
{
    using System.Collections.Generic;
    using Domain.Model.Products;

    public interface IProductPriceService
    {
        void DeletePrice(ProductPrice price);
        IList<ProductPrice> GetProductPricesByIdProduct(int productId);
        IList<ProductPrice> GetProductPriceList(int storeId, int productId);
        ProductPrice GetProductPrice(int storeId, int productId);
        void InsertPrice(ProductPrice price);
        void UpdatePrice(ProductPrice price);
    }
}