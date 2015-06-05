namespace Doamin.Service.Products
{
    using System.Collections.Generic;
    using Domain.Model.Products;
    using EasyErp.Core;

    public interface IProductStoreMappingService
    {
        IList<ProductStoreMapping> GetProductStoreMappings(string productName, int categoryId, int storeId);
        void InsertInventor(ProductStoreMapping psm);
    }
}