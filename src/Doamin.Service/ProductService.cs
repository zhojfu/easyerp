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

        public ProductService(IRepository<Product> repository)
        {
            this.repository = repository;
        }

        public Product GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Product> GetProductsByIds(int[] ids)
        {
            return this.repository.FindAll(a => a.Name.Contains("cake")).ToList();
        }

        public void AddNewProduct(Product product)
        {
            this.repository.Add(product);
            this.repository.Update();
        }

        private Product ConvertViewModelToModel()
        {
            return null;
        }
    }
}