namespace Doamin.Service
{
    using Domain.Model;
    using Infrastructure.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductService
    {
        private readonly IRepository<Price> priceRepository;

        private readonly IRepository<Product> repository;

        private readonly IUnitOfWork unitOfWork;

        public ProductService(
            IRepository<Product> repository,
            IRepository<Price> priceRepository,
            IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.priceRepository = priceRepository;
            this.unitOfWork = unitOfWork;
        }

        //public Product GetProductById(int id)
        //{
        //    var query = from p in this.pricerepository.
        //}

        public List<Price> GetPricesByProductId(Guid productId)
        {
            return this.priceRepository.FindAll(i => i.ProductId == productId).OrderBy(p => p.UpdataTime).ToList();
        }

        public IList<Product> GetProductsByIds(Guid[] ids)
        {
            return this.repository.FindAll(a => ids.Contains(a.Id)).ToList();
        }

        public IList<Product> GetAllProducts()
        {
            // return null;
            return this.repository.FindAll(a => true).ToList();
        }

        public void AddNewProduct(Product product)
        {
            this.repository.Add(product);
            this.repository.Update();
        }

        //public void AddTestDouble(TestDoubles test)
        //{
        //    this.repository.Add(test);
        //    this.unitOfWork.Commit();
        //}

        private Product ConvertViewModelToModel()
        {
            return null;
        }
    }
}