namespace Doamin.Service.Products
{
    using Domain.Model.Payment;
    using Domain.Model.Products;
    using System.Collections.Generic;

    public interface IInventoryService
    {
        IList<Inventory> GetAllInventoriesForProduct(int productId);

        void InsertInventory(Inventory inventory, Payment payment);
    }
}