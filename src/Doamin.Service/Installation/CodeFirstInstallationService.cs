namespace Doamin.Service.Installation
{
    using Domain.Model;
    using Infrastructure.Domain;
    using System.Collections.Generic;

    public class CodeFirstInstallationService : IInstallationService
    {
        public readonly IRepository<Order> OrderRepository;

        public readonly IRepository<Product> ProducrRepository;

        public CodeFirstInstallationService(IRepository<Product> productRepository, IRepository<Order> orderRepository)
        {
            this.ProducrRepository = productRepository;
            this.OrderRepository = orderRepository;
        }

        public void InstallData()
        {
            this.InstallProducts();
        }

        private void InstallProducts()
        {
            var products = new List<Product>();

            products.ForEach(p => this.ProducrRepository.Add(p));
            this.ProducrRepository.Update();
        }

        private void InstallOrder()
        {
        }
    }
}