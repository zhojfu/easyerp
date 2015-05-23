namespace Doamin.Service.Products
{
    using Domain.Model.Products;
    using EasyErp.Core;
    using Infrastructure.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductService : IProductService
    {
        private readonly IRepository<Product> productRepository;

        private readonly IUnitOfWork unitOfWork;

        public ProductService(IRepository<Product> productRepository, IUnitOfWork unitOfWork)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }

        public void DeleteProduct(Product product)
        {
            productRepository.Remove(product);
        }

        public Product GetProductById(long productId)
        {
            return productId == 0 ? null : productRepository.GetByKey(productId);
        }

        public IList<Product> GetProductsByIds(int[] productIds)
        {
            if (productIds == null ||
                !productIds.Any())
            {
                return new List<Product>();
            }

            return productRepository.FindAll(p => productIds.Contains(p.Id)).ToList();
        }

        public IList<Product> GetAllProducts()
        {
            return productRepository.FindAll(p => true).ToList();
        }

        public IPagedList<Product> SearchProducts(
            int pageIndex = 0,
            int pageSize = Int32.MaxValue,
            IList<int> categoryIds = null,
            IList<int> storeIds = null,
            string keywords = null,
            bool searchSku = true,
            bool? published = null)
        {
            if (categoryIds != null &&
                categoryIds.Contains(0))
            {
                categoryIds.Remove(0);
            }
            if (storeIds != null &&
                storeIds.Contains(0))
            {
                storeIds.Remove(0);
            }

            var products = productRepository.FindAll(p => !p.Deleted);

            if (published.HasValue &&
                published.Value)
            {
                products = products.Where(p => p.Published == published);
            }

            if (!string.IsNullOrEmpty(keywords))
            {
                products = products.Where(
                    p =>
                    p.Name.Contains(keywords) || p.FullDescription.Contains(keywords) ||
                    p.ShortDescription.Contains(keywords));
            }

            if (categoryIds != null &&
                categoryIds.Any())
            {
                products = products.Where(p => categoryIds.Contains(p.CategoryId));
            }

            if (storeIds != null &&
                storeIds.Any())
            {
                products = products.Where(p => p.Stores.Any(ps => storeIds.Contains(ps.Id)));
            }

            return new PagedList<Product>(products.OrderBy(p => p.CategoryId), pageIndex, pageSize);
        }

        public void InsertProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("product");
            }

            //insert
            productRepository.Add(product);
            unitOfWork.Commit();
        }

        public void UpdateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("product");
            }

            productRepository.Update(product);
            unitOfWork.Commit();
        }

        public Product GetProductByGtin(string gtin)
        {
            return string.IsNullOrEmpty(gtin) ? null : productRepository.FindAll(p => p.Gtin == gtin).FirstOrDefault();
        }
    }
}