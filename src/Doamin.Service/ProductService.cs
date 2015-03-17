namespace Doamin.Service
{
    using Domain.Model;
    using Infrastructure.Domain;
    using System;
    using System.Collections.Generic;

    public class ProductService
    {
        private IRepository<Product> repository;

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
            throw new NotImplementedException();
        }

        public void AddNewProduct( /*ViewProduct viewModel*/)
        {
        }

        private Product ConvertViewModelToModel()
        {
            return null;
        }
    }
}