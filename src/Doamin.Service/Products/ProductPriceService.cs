namespace Doamin.Service.Products
{
    using Domain.Model.Products;
    using EasyErp.Core;
    using Infrastructure.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductPriceService : IProductPriceService
    {
        private IRepository<ProductPrice> priceRepository;

        private IUnitOfWork unitOfWork;

        public ProductPriceService(IRepository<ProductPrice> priceRepository, IUnitOfWork unitOfWork)
        {
            this.priceRepository = priceRepository;
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
            return productId < 0 ? new List<ProductPrice>() : priceRepository.FindAll(i => i.ProductId == productId).ToList();
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
    }
}