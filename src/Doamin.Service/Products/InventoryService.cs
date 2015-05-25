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

        public void InsertInventory(Inventory inventory, Payment payment)
        {
            inventoryRepository.Add(inventory);
            var product = productRepository.GetByKey(inventory.ProductId);
            product.StockQuantity += inventory.Quantity;

            productRepository.Update(product);

            unitOfWork.Commit();
        }
    }
}