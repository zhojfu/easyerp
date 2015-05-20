namespace Doamin.Service.Products
{
    using Domain.Model.Products;
    using EasyErp.Core;
    using System.Collections.Generic;

    public interface IProductService
    {
        #region Products

        void DeleteProduct(Product product);

        Product GetProductById(long productId);

        IEnumerable<Product> GetAutoCompleteProducts(string name);

        IList<Product> GetProductsByIds(int[] productIds);

        void InsertProduct(Product product);

        void UpdateProduct(Product product);

        IList<Product> GetAllProducts();

        IPagedList<Product> SearchProducts(
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            IList<int> categoryIds = null,
            IList<int> storeIds = null,
            string keywords = null,
            bool searchSku = true,
            bool? overridePublished = null);

        Product GetProductByGtin(string gtin);

        #endregion Products
    }
}