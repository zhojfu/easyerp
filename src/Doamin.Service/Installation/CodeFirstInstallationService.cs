namespace Doamin.Service.Installation
{
    using System.Collections.Generic;
    using Domain.Model.Orders;
    using Domain.Model.Products;
    using Infrastructure.Domain;

    public class CodeFirstInstallationService : IInstallationService
    {
        private readonly IRepository<Category> categoryRepository;

        private readonly IRepository<Order> orderRepository;

        private readonly IRepository<Product> producrRepository;

        public CodeFirstInstallationService(
            IRepository<Product> productRepository,
            IRepository<Order> orderRepository,
            IRepository<Category> categoryRepository)
        {
            producrRepository = productRepository;
            this.orderRepository = orderRepository;
            this.categoryRepository = categoryRepository;
        }

        public void InstallData()
        {
            InstallProducts();
        }

        private void InstallProducts()
        {
            var products = new List<Product>();

            products.ForEach(p => producrRepository.Add(p));
            producrRepository.Update();
        }

        protected virtual void InstallCategories()
        {
            var allCategories = new List<Category>
            {
                new Category
                {
                    Name = "Rice"

                    //Descriiption = "Rice category"
                },
                new Category
                {
                    Name = "Food Oil"

                    //Descriiption = "Food Oil category"
                },
                new Category
                {
                    Name = "Other"

                    //Descriiption = "Other category"
                }
            };

            allCategories.ForEach(c => categoryRepository.Add(c));
            categoryRepository.Update();
        }

        private void InstallOrder()
        {
        }
    }
}