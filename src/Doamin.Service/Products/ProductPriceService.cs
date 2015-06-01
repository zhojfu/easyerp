namespace Doamin.Service.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Model.Products;
    using Infrastructure.Domain;

    public class ProductPriceService : IProductPriceService
    {
        private readonly IRepository<ProductPrice> priceRepository;
        private readonly IRepository<Product> productRepository;

        private readonly IUnitOfWork unitOfWork;

        public ProductPriceService(IRepository<ProductPrice> priceRepository, IRepository<Product> productRepository, IUnitOfWork unitOfWork)
        {
            this.priceRepository = priceRepository;
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }

        public void DeletePrice(ProductPrice price)
        {
            if (price == null)
            {
                throw new ArgumentNullException("price");
            }
            priceRepository.Remove(price);

            unitOfWork.Commit();
        }

        public IList<ProductPrice> GetProductPricesByIdProduct(int productId)
        {
            return productId < 0
                       ? new List<ProductPrice>()
                       : priceRepository.FindAll(i => i.ProductId == productId).ToList();
        }

        public void InsertPrice(ProductPrice price)
        {
            if (price == null)
            {
                throw new ArgumentNullException("price");
            }

            priceRepository.Add(price);

            unitOfWork.Commit();
        }

        public void UpdatePrice(ProductPrice price)
        {
            if (price == null)
            {
                throw new ArgumentNullException("price");
            }

            priceRepository.Update(price);

            unitOfWork.Commit();
        }


        public IList<ProductPrice> GetProductPriceList(int storeId, int productId)
        {
            if (productId < 1 || storeId < 1)
            {
                return null;
            }

            return priceRepository.FindAll(i => i.ProductId == productId && i.StoreId == storeId).ToList();
        }

        public ProductPrice GetProductPrice(int storeId, int productId)
        {
            
            if (productId < 1 || storeId < 1)
            {
                return null;
            }

            var price = priceRepository.FindAll(i => i.ProductId == productId && i.StoreId == storeId)
                .OrderByDescending(p => p.DateTime).FirstOrDefault();

            if (price == null)
            {
                var product = productRepository.GetByKey(productId);
                price = new ProductPrice
                {
                    CostPrice = product.ProductCost,
                    SalePrice = product.Price,
                    StoreId = storeId,
                    ProductId = productId
                };
            }

            return price;
        }
    }
}