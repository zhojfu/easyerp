namespace Doamin.Service
{
    using Domain.Model;
    using Infrastructure.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductService
    {
        private readonly IRepository<Product> repository;

        private readonly IUnitOfWork unitOfWork;

        public ProductService(IRepository<Product> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public Product GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Product> GetProductsByIds(int[] ids)
        {
            return null;
            //return this.repository.FindAll(a => a.Name.Contains("cake")).ToList();
        }

        public IList<Product> GetAllProducts()
        {
           // return null;
            return this.repository.FindAll(a => a.Name.Contains("cake")).ToList();
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