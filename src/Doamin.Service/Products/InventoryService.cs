using System;

namespace Doamin.Service.Products
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Model.Payments;
    using Domain.Model.Products;
    using Infrastructure.Domain;

    public class InventoryService : IInventoryService
    {
        private readonly IRepository<Inventory> inventoryRepository;

        private readonly IRepository<Product> productRepository;

        private readonly IUnitOfWork unitOfWork;

        public InventoryService(
            IRepository<Product> productRepository,
            IRepository<Inventory> inventoryRepository,
            IUnitOfWork unitOfWork)
        {
            this.productRepository = productRepository;
            this.inventoryRepository = inventoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public IList<Inventory> GetAllInventoriesForProduct(int productId)
        {
            return productId <= 0
                       ? new List<Inventory>()
                       : inventoryRepository.FindAll(i => i.ProductId == productId).ToList();
        }

        public float GetProductQuantity(int productId, int storeId)
        {
            if (productId <= 0 || storeId <= 0)
            {
                return 0;
            }

            var psm = inventoryRepository.FindAll(i => i.ProductId == storeId && i.StoreId == storeId).ToList();

            return !psm.Any() ? 0 : psm.Sum(p => p.Quantity);
        }

        public void InsertInventory(Inventory inventory)
        {
            if (inventory == null)
            {
                throw new ArgumentNullException("inventory");
            }
            inventoryRepository.Add(inventory);
            unitOfWork.Commit();
        }
        public IList<StockModel> GetProductInventories(string productName, int categoryId, int storeId)
        {

            var psm = inventoryRepository.FindAll(i=>true);
            if (storeId > 0)
            {
                psm = psm.Where(i => i.StoreId == storeId);
            }

            if (categoryId >0)
            {
                psm = psm.Where(i => i.Product.CategoryId == categoryId);
            }

            if (!string.IsNullOrWhiteSpace(productName))
            {
                psm = psm.Where(i => i.Product.Name.Contains(productName));
            }

            return psm.GroupBy(p => new {p.StoreId, p.ProductId})
                .Select(l => new StockModel
                {
                    Store = l.FirstOrDefault().Store,
                    Product = l.FirstOrDefault().Product,
                    ProductId = l.FirstOrDefault().ProductId,
                    Quantity = l.Sum(cc => cc.Quantity),
                    StoreId = l.FirstOrDefault().StoreId
                }).ToList();

        }
        public IList<Inventory> GetProductInventoryRecords(string productName, int categoryId, int storeId, bool unpaidOnly = false)
        {

            var psm = inventoryRepository.FindAll(i=>true);
            if (storeId > 0)
            {
                psm = psm.Where(i => i.StoreId == storeId);
            }

            if (categoryId >0)
            {
                psm = psm.Where(i => i.Product.CategoryId == categoryId);
            }

            if (!string.IsNullOrWhiteSpace(productName))
            {
                psm = psm.Where(i => i.Product.Name.Contains(productName));
            }

            return psm.ToList();
        }
    }
}