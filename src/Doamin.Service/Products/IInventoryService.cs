namespace Doamin.Service.Products
{
    using System.Collections.Generic;
    using Domain.Model.Payments;
    using Domain.Model.Products;

    public interface IInventoryService
    {
        IList<Inventory> GetAllInventoriesForProduct(int productId);
        void InsertInventory(Inventory inventory, Payment payment);
    }
}