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
            return new List<Product>
            {
                new Product
                {
                    Id = new Guid(),
                    Cost = 5,
                    Description = "pancake description",
                    Name = "pancake",
                    Origin = "china",
                    Price = 10,
                    Upc = "690193900",
                    Volume = 1.0
                },
                new Product
                {
                    Id = new Guid(),
                    Cost = 6,
                    Description = "pancake description",
                    Name = "cupcake",
                    Origin = "china",
                    Price = 15,
                    Upc = "690193901",
                    Volume = 1.0
                },
                new Product
                {
                    Id = new Guid(),
                    Cost = 7,
                    Description = "cup description",
                    Name = "cup",
                    Origin = "china",
                    Price = 18,
                    Upc = "690193902",
                    Volume = 1.0
                },
                new Product
                {
                    Id = new Guid(),
                    Cost = 9,
                    Description = "pan description",
                    Name = "pan",
                    Origin = "china",
                    Price = 20,
                    Upc = "690193903",
                    Volume = 1.0
                },
                new Product
                {
                    Id = new Guid(),
                    Cost = 25,
                    Description = "cake description",
                    Name = "cake",
                    Origin = "china",
                    Price = 40,
                    Upc = "690193901",
                    Volume = 1.0
                }
            };
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