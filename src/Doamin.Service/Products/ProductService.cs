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
            this.productRepository.Remove(product);
        }

        public Product GetProductById(long productId)
        {
            if (productId == 0)
                return null;
            
            return productRepository.GetByKey(productId);
        }

        public IList<Product> GetProductsByIds(int[] productIds)
        {
            throw new NotImplementedException();
        }

        public void InsertProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            //insert
            this.productRepository.Add(product);
            this.unitOfWork.Commit();
        }

        public void UpdateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            //update
            productRepository.Update(product);
            this.unitOfWork.Commit();
        }

        public int GetCategoryProductNumber(IList<int> categoryIds = null, int storeId = 0)
        {
            throw new NotImplementedException();
        }

        public IPagedList<Product> SearchProducts(
            int pageIndex = 0,
            int pageSize = Int32.MaxValue,
            IList<int> categoryIds = null,
            int manufacturerId = 0,
            int storeId = 0,
            int vendorId = 0,
            int warehouseId = 0,
            bool visibleIndividuallyOnly = false,
            bool? featuredProducts = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            int productTagId = 0,
            string keywords = null,
            bool searchDescriptions = false,
            bool searchSku = true,
            bool searchProductTags = false,
            int languageId = 0,
            IList<int> filteredSpecs = null,
            ProductSortingEnum orderBy = ProductSortingEnum.Position,
            bool showHidden = false,
            bool? overridePublished = null)
        {
            return new PagedList<Product>(this.productRepository.FindAll(p => true).ToList(), 0, 10);
        }

        public IPagedList<Product> SearchProducts(
            out IList<int> filterableSpecificationAttributeOptionIds,
            bool loadFilterableSpecificationAttributeOptionIds = false,
            int pageIndex = 0,
            int pageSize = Int32.MaxValue,
            IList<int> categoryIds = null,
            int manufacturerId = 0,
            int storeId = 0,
            int vendorId = 0,
            int warehouseId = 0,
            int? productType = null,
            bool visibleIndividuallyOnly = false,
            bool? featuredProducts = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            int productTagId = 0,
            string keywords = null,
            bool searchDescriptions = false,
            bool searchSku = true,
            bool searchProductTags = false,
            int languageId = 0,
            IList<int> filteredSpecs = null,
            ProductSortingEnum orderBy = ProductSortingEnum.Position,
            bool showHidden = false,
            bool? overridePublished = null)
        {
            filterableSpecificationAttributeOptionIds = null;
            return new PagedList<Product>(this.productRepository.FindAll(p => true).ToList(), 1, 10);
        }

        public IList<Product> GetAssociatedProducts(int parentGroupedProductId, int storeId = 0, int vendorId = 0, bool showHidden = false)
        {
            return new PagedList<Product>(this.productRepository.FindAll(p => true).ToList(), 1, 10);
        }

        public void UpdateProductReviewTotals(Product product)
        {
            throw new NotImplementedException();
        }

        public void GetLowStockProducts(int vendorId, out IList<Product> products, out IList<ProductAttributeCombination> combinations)
        {
            throw new NotImplementedException();
        }

        public Product GetProductBySku(string sku)
        {
            throw new NotImplementedException();
        }

        public void UpdateHasTierPricesProperty(Product product)
        {
            throw new NotImplementedException();
        }

        public void UpdateHasDiscountsApplied(Product product)
        {
            throw new NotImplementedException();
        }

        public void AdjustInventory(Product product, int quantityToChange, string attributesXml = "")
        {
            throw new NotImplementedException();
        }

        public void ReserveInventory(Product product, int quantity)
        {
            throw new NotImplementedException();
        }

        public void UnblockReservedInventory(Product product, int quantity)
        {
            throw new NotImplementedException();
        }

        public void BookReservedInventory(Product product, int warehouseId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}