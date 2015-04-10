namespace Doamin.Service.Products
{
    using Domain.Model.Products;
    using EasyErp.Core;
    using System;
    using System.Collections.Generic;

    public interface IProductService
    {
        #region Products

        void DeleteProduct(Product product);

        Product GetProductById(long productId);

        IList<Product> GetProductsByIds(int[] productIds);

        void InsertProduct(Product product);

        void UpdateProduct(Product product);

        int GetCategoryProductNumber(IList<int> categoryIds = null, int storeId = 0);

        IPagedList<Product> SearchProducts(
             int pageIndex = 0,
             int pageSize = int.MaxValue,
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
             bool? overridePublished = null);

        IPagedList<Product> SearchProducts(
            out IList<int> filterableSpecificationAttributeOptionIds,
            bool loadFilterableSpecificationAttributeOptionIds = false,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
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
            bool? overridePublished = null);

        IList<Product> GetAssociatedProducts(int parentGroupedProductId,
            int storeId = 0, int vendorId = 0, bool showHidden = false);

        void UpdateProductReviewTotals(Product product);

        void GetLowStockProducts(int vendorId,
            out IList<Product> products,
            out IList<ProductAttributeCombination> combinations);

        /// <summary>
        /// Gets a product by SKU
        /// </summary>
        /// <param name="sku">SKU</param>
        /// <returns>Product</returns>
        Product GetProductBySku(string sku);

        /// <summary>
        /// Update HasTierPrices property (used for performance optimization)
        /// </summary>
        /// <param name="product">Product</param>
        void UpdateHasTierPricesProperty(Product product);

        /// <summary>
        /// Update HasDiscountsApplied property (used for performance optimization)
        /// </summary>
        /// <param name="product">Product</param>
        void UpdateHasDiscountsApplied(Product product);

        #endregion Products

        #region Inventory management methods

        /// <summary>
        /// Adjust inventory
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="quantityToChange">Quantity to increase or descrease</param>
        /// <param name="attributesXml">Attributes in XML format</param>
        void AdjustInventory(Product product, int quantityToChange, string attributesXml = "");

        /// <summary>
        /// Reserve the given quantity in the warehouses.
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="quantity">Quantity, must be negative</param>
        void ReserveInventory(Product product, int quantity);

        /// <summary>
        /// Unblocks the given quantity reserved items in the warehouses
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="quantity">Quantity, must be positive</param>
        void UnblockReservedInventory(Product product, int quantity);

        /// <summary>
        /// Book the reserved quantity
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="warehouseId">Warehouse identifier</param>
        /// <param name="quantity">Quantity, must be negative</param>
        void BookReservedInventory(Product product, int warehouseId, int quantity);

        #endregion Inventory management methods
    }
}