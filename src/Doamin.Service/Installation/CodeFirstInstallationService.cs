namespace Doamin.Service.Installation
{
    using Domain.Model;
    using Infrastructure.Domain;
    using System.Collections.Generic;

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
            this.producrRepository = productRepository;
            this.orderRepository = orderRepository;
            this.categoryRepository = categoryRepository;
        }

        public void InstallData()
        {
            this.InstallProducts();
        }

        private void InstallProducts()
        {
            var products = new List<Product>();

            products.ForEach(p => this.producrRepository.Add(p));
            this.producrRepository.Update();
        }

        protected virtual void InstallCategories()
        {
            var allCategories = new List<Category>
            {
                new Category
                {
                    Name = "Rice",
                    Descriiption = "Rice category"
                },
                new Category
                {
                    Name = "Food Oil",
                    Descriiption = "Food Oil category"
                },
                new Category
                {
                    Name = "Other",
                    Descriiption = "Other category"
                }
            };

            allCategories.ForEach(c => this.categoryRepository.Add(c));
            this.categoryRepository.Update();
        }

        private void InstallOrder()
        {
        }
    }
}