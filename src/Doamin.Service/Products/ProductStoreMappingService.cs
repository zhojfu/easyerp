namespace Doamin.Service.Products
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Model.Products;
    using Infrastructure.Domain;

    public class ProductStoreMappingService : IProductStoreMappingService
    {
        private readonly IRepository<ProductStoreMapping> productStoreMappingRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProductStoreMappingService(IRepository<ProductStoreMapping> psmRepository,
            IUnitOfWork uof)
        {
            productStoreMappingRepository = psmRepository;
            unitOfWork = uof;
        }
        public IList<ProductStoreMapping> GetProductStoreMappings(string productName, int categoryId, int storeId)
        {

            var psm = productStoreMappingRepository.FindAll(i=>true);
            if (storeId > 0)
            {
                psm = psm.Where(i => i.StoreId == storeId);
            }

            if (categoryId >0)
            {
                psm = psm.Where(i => i.Product.CategoryId == categoryId);
            }

            if (!string.IsNullOrWhiteSpace(productName))
            {
                psm = psm.Where(i => i.Product.Name.Contains(productName));
            }

            return psm.ToList();
        }


        public void InsertInventor(ProductStoreMapping productStoreMapping)
        {
            if (productStoreMapping == null)
            {
                throw new ArgumentNullException("productStoreMapping");
            }

            productStoreMappingRepository.Add(productStoreMapping);
            unitOfWork.Commit();
            
        }
    }
}