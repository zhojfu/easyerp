namespace Doamin.Service.Products
{
    using Domain.Model.Payment;
    using Domain.Model.Products;
    using Infrastructure.Domain;
    using System;
    using System.Collections.Generic;

    public class InventoryService : IInventoryService
    {
        private readonly IRepository<Product> productRepository;

        private readonly IRepository<Inventory> inventoryRepository;
        private readonly IRepository<Payment> paymentRepository;

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
            this.inventoryRepository.Add(inventory);
            var product = this.productRepository.GetByKey(inventory.ProductId);
            product.StockQuantity += inventory.Quantity;
            this.paymentRepository.Add(payment);

            this.productRepository.Update(product);

            this.unitOfWork.Commit();
        }
    }
}