namespace Doamin.Service.Products
{
    using Domain.Model.Payments;
    using Domain.Model.Products;
    using Infrastructure.Domain;
    using System;
    using System.Collections.Generic;

    public class InventoryService : IInventoryService
    {
        private readonly IRepository<Inventory> inventoryRepository;

        private readonly IRepository<Payment> paymentRepository;

        private readonly IRepository<Product> productRepository;

        private readonly IUnitOfWork unitOfWork;

        public InventoryService(
            IRepository<Product> productRepository,
            IRepository<Inventory> inventoryRepository,
            IRepository<Payment> paymentRepository,
            IUnitOfWork unitOfWork)
        {
            this.productRepository = productRepository;
            this.inventoryRepository = inventoryRepository;
            this.paymentRepository = paymentRepository;
            this.unitOfWork = unitOfWork;
        }

        public IList<Inventory> GetAllInventoriesForProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public void InsertInventory(Inventory inventory, Payment payment)
        {
            inventoryRepository.Add(inventory);
            var product = productRepository.GetByKey(inventory.ProductId);
            product.StockQuantity += inventory.Quantity;
            //paymentRepository.Add(payment);

            productRepository.Update(product);

            unitOfWork.Commit();
        }
    }
}