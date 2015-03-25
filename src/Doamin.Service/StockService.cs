namespace Doamin.Service
{
    using Domain.Model;
    using Infrastructure.Domain;
    using System.Collections.Generic;
    using System.Linq;

    public class StockService
    {
        private readonly IRepository<Product> productRepository;

        private readonly IRepository<RepositoryStock> repository;

        private readonly IUnitOfWork unitOfWork;

        public StockService(
            IRepository<RepositoryStock> repository,
            IRepository<Product> productRepository,
            IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }

        public IList<RepositoryStock> GetAllStockList()
        {
            return this.repository.FindAll(i => i.Id != null).ToList();
        }

        public void AddStock(RepositoryStock stock)
        {
            var product = this.productRepository.GetByKey(stock.ProductId);
            if (product == null)
            {
                this.productRepository.Add(
                    new Product
                    {
                        Id = stock.ProductId,
                        Upc = "test"
                    });
            }

            this.repository.Add(stock);
            this.unitOfWork.Commit();
        }
    }
}