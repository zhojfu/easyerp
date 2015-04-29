namespace Doamin.Service.Products
{
    using Domain.Model.Products;
    using EasyErp.Core;
    using System.Collections.Generic;

    public interface IProductPriceService
    {
        void DeletePrice(ProductPrice price);

        IList<ProductPrice> GetProductPricesByIdProduct(int productId);

        void InsertPrice(ProductPrice price);

        void UpdatePrice(ProductPrice price);
    }
}