namespace Doamin.Service.Products
{
    using System.Collections.Generic;
    using Domain.Model.Payments;
    using Domain.Model.Products;

    public interface IInventoryService
    {
        IList<Inventory> GetAllInventoriesForProduct(int productId);
        IList<Inventory> GetProductInventoryRecords(string productName, int categoryId, int storeId, bool unpaidOnly = false);
        IList<StockModel> GetProductInventories(string productName, int categoryId, int storeId);
        void InsertInventory(Inventory inventory);
    }
}