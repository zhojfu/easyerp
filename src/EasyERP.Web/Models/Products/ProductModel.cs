namespace EasyERP.Web.Models.Products
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using EasyERP.Web.Validators.Products;
    using FluentValidation.Attributes;
    using Infrastructure.Domain.Model;

    [Validator(typeof(ProductValidator))]
    public class ProductModel : BaseEntity
    {
        public ProductModel()
        {
            AvailableCategories = new List<SelectListItem>();
        }

        public override int Id { get; set; }

        [AllowHtml]
        public string Name { get; set; }

        [AllowHtml]
        public string ShortDescription { get; set; }

        [AllowHtml]
        public string FullDescription { get; set; }

        [AllowHtml]
        public string Sku { get; set; }

        [AllowHtml]
        public virtual string Gtin { get; set; }

        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public decimal ProductCost { get; set; }
        public decimal Weight { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public bool Published { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }

        //categories
        public IList<SelectListItem> AvailableCategories { get; set; }

        #region Nested classes

        public class AddRequiredProductModel : BaseEntity
        {
            public AddRequiredProductModel()
            {
                AvailableCategories = new List<SelectListItem>();
                AvailableManufacturers = new List<SelectListItem>();
                AvailableStores = new List<SelectListItem>();
                AvailableVendors = new List<SelectListItem>();
                AvailableProductTypes = new List<SelectListItem>();
            }

            [AllowHtml]
            public string SearchProductName { get; set; }

            public int SearchCategoryId { get; set; }
            public int SearchManufacturerId { get; set; }
            public int SearchStoreId { get; set; }
            public int SearchVendorId { get; set; }
            public int SearchProductTypeId { get; set; }
            public IList<SelectListItem> AvailableCategories { get; set; }
            public IList<SelectListItem> AvailableManufacturers { get; set; }
            public IList<SelectListItem> AvailableStores { get; set; }
            public IList<SelectListItem> AvailableVendors { get; set; }
            public IList<SelectListItem> AvailableProductTypes { get; set; }

            //vendor
            public bool IsLoggedInAsVendor { get; set; }
        }

        public class AddProductSpecificationAttributeModel : BaseEntity
        {
            public AddProductSpecificationAttributeModel()
            {
                AvailableAttributes = new List<SelectListItem>();
                AvailableOptions = new List<SelectListItem>();
            }

            public int SpecificationAttributeId { get; set; }
            public int AttributeTypeId { get; set; }
            public int SpecificationAttributeOptionId { get; set; }

            [AllowHtml]
            public string CustomValue { get; set; }

            public bool AllowFiltering { get; set; }
            public bool ShowOnProductPage { get; set; }
            public int DisplayOrder { get; set; }
            public IList<SelectListItem> AvailableAttributes { get; set; }
            public IList<SelectListItem> AvailableOptions { get; set; }
        }

        public class ProductPictureModel : BaseEntity
        {
            public int ProductId { get; set; }
            public int PictureId { get; set; }
            public string PictureUrl { get; set; }
            public int DisplayOrder { get; set; }
        }

        public class ProductCategoryModel : BaseEntity
        {
            public string Category { get; set; }
            public int ProductId { get; set; }
            public int CategoryId { get; set; }
            public bool IsFeaturedProduct { get; set; }
            public int DisplayOrder { get; set; }
        }

        public class ProductManufacturerModel : BaseEntity
        {
            public string Manufacturer { get; set; }
            public int ProductId { get; set; }
            public int ManufacturerId { get; set; }
            public bool IsFeaturedProduct { get; set; }
            public int DisplayOrder { get; set; }
        }

        public class RelatedProductModel : BaseEntity
        {
            public int ProductId2 { get; set; }
            public string Product2Name { get; set; }
            public int DisplayOrder { get; set; }
        }

        public class AddRelatedProductModel : BaseEntity
        {
            public AddRelatedProductModel()
            {
                AvailableCategories = new List<SelectListItem>();
                AvailableManufacturers = new List<SelectListItem>();
                AvailableStores = new List<SelectListItem>();
                AvailableVendors = new List<SelectListItem>();
                AvailableProductTypes = new List<SelectListItem>();
            }

            [AllowHtml]
            public string SearchProductName { get; set; }

            public int SearchCategoryId { get; set; }
            public int SearchManufacturerId { get; set; }
            public int SearchStoreId { get; set; }
            public int SearchVendorId { get; set; }
            public int SearchProductTypeId { get; set; }
            public IList<SelectListItem> AvailableCategories { get; set; }
            public IList<SelectListItem> AvailableManufacturers { get; set; }
            public IList<SelectListItem> AvailableStores { get; set; }
            public IList<SelectListItem> AvailableVendors { get; set; }
            public IList<SelectListItem> AvailableProductTypes { get; set; }
            public int ProductId { get; set; }
            public int[] SelectedProductIds { get; set; }

            //vendor
            public bool IsLoggedInAsVendor { get; set; }
        }

        public class AssociatedProductModel : BaseEntity
        {
            public string ProductName { get; set; }
            public int DisplayOrder { get; set; }
        }

        public class AddAssociatedProductModel : BaseEntity
        {
            public AddAssociatedProductModel()
            {
                AvailableCategories = new List<SelectListItem>();
                AvailableManufacturers = new List<SelectListItem>();
                AvailableStores = new List<SelectListItem>();
                AvailableVendors = new List<SelectListItem>();
                AvailableProductTypes = new List<SelectListItem>();
            }

            [AllowHtml]
            public string SearchProductName { get; set; }

            public int SearchCategoryId { get; set; }
            public int SearchManufacturerId { get; set; }
            public int SearchStoreId { get; set; }
            public int SearchVendorId { get; set; }
            public int SearchProductTypeId { get; set; }
            public IList<SelectListItem> AvailableCategories { get; set; }
            public IList<SelectListItem> AvailableManufacturers { get; set; }
            public IList<SelectListItem> AvailableStores { get; set; }
            public IList<SelectListItem> AvailableVendors { get; set; }
            public IList<SelectListItem> AvailableProductTypes { get; set; }
            public int ProductId { get; set; }
            public int[] SelectedProductIds { get; set; }

            //vendor
            public bool IsLoggedInAsVendor { get; set; }
        }

        public class CrossSellProductModel : BaseEntity
        {
            public int ProductId2 { get; set; }
            public string Product2Name { get; set; }
        }

        public class AddCrossSellProductModel : BaseEntity
        {
            public AddCrossSellProductModel()
            {
                AvailableCategories = new List<SelectListItem>();
                AvailableManufacturers = new List<SelectListItem>();
                AvailableStores = new List<SelectListItem>();
                AvailableVendors = new List<SelectListItem>();
                AvailableProductTypes = new List<SelectListItem>();
            }

            [AllowHtml]
            public string SearchProductName { get; set; }

            public int SearchCategoryId { get; set; }
            public int SearchManufacturerId { get; set; }
            public int SearchStoreId { get; set; }
            public int SearchVendorId { get; set; }
            public int SearchProductTypeId { get; set; }
            public IList<SelectListItem> AvailableCategories { get; set; }
            public IList<SelectListItem> AvailableManufacturers { get; set; }
            public IList<SelectListItem> AvailableStores { get; set; }
            public IList<SelectListItem> AvailableVendors { get; set; }
            public IList<SelectListItem> AvailableProductTypes { get; set; }
            public int ProductId { get; set; }
            public int[] SelectedProductIds { get; set; }

            //vendor
            public bool IsLoggedInAsVendor { get; set; }
        }

        public class TierPriceModel : BaseEntity
        {
            public int ProductId { get; set; }
            public int CustomerRoleId { get; set; }
            public string CustomerRole { get; set; }
            public int StoreId { get; set; }
            public string Store { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }

        public class ProductWarehouseInventoryModel : BaseEntity
        {
            public int WarehouseId { get; set; }
            public string WarehouseName { get; set; }
            public bool WarehouseUsed { get; set; }
            public int StockQuantity { get; set; }
            public int ReservedQuantity { get; set; }
            public int PlannedQuantity { get; set; }
        }

        public class ProductAttributeMappingModel : BaseEntity
        {
            public int ProductId { get; set; }
            public int ProductAttributeId { get; set; }
            public string ProductAttribute { get; set; }

            [AllowHtml]
            public string TextPrompt { get; set; }

            public bool IsRequired { get; set; }
            public int AttributeControlTypeId { get; set; }
            public string AttributeControlType { get; set; }
            public int DisplayOrder { get; set; }
            public string ViewEditValuesUrl { get; set; }
            public string ViewEditValuesText { get; set; }

            //validation fields

            public bool ValidationRulesAllowed { get; set; }
            public int? ValidationMinLength { get; set; }
            public int? ValidationMaxLength { get; set; }
            public string ValidationFileAllowedExtensions { get; set; }
            public int? ValidationFileMaximumSize { get; set; }
            public string DefaultValue { get; set; }
        }

        public class ProductAttributeValueListModel : BaseEntity
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int ProductAttributeMappingId { get; set; }
            public string ProductAttributeName { get; set; }
        }

        [Validator(typeof(ProductAttributeValueModelValidator))]
        public class ProductAttributeValueModel : BaseEntity
        {
            public ProductAttributeValueModel()
            {
                ProductPictureModels = new List<ProductPictureModel>();
            }

            public int ProductAttributeMappingId { get; set; }
            public int AttributeValueTypeId { get; set; }
            public string AttributeValueTypeName { get; set; }
            public int AssociatedProductId { get; set; }
            public string AssociatedProductName { get; set; }

            [AllowHtml]
            public string Name { get; set; }

            [AllowHtml]
            public string ColorSquaresRgb { get; set; }

            public bool DisplayColorSquaresRgb { get; set; }
            public decimal PriceAdjustment { get; set; }

            //used only on the values list page
            public string PriceAdjustmentStr { get; set; }
            public decimal WeightAdjustment { get; set; }

            //used only on the values list page
            public string WeightAdjustmentStr { get; set; }
            public decimal Cost { get; set; }
            public int Quantity { get; set; }
            public bool IsPreSelected { get; set; }
            public int DisplayOrder { get; set; }
            public int PictureId { get; set; }
            public string PictureThumbnailUrl { get; set; }
            public IList<ProductPictureModel> ProductPictureModels { get; set; }

            #region Nested classes

            public class AssociateProductToAttributeValueModel : BaseEntity
            {
                public AssociateProductToAttributeValueModel()
                {
                    AvailableCategories = new List<SelectListItem>();
                    AvailableManufacturers = new List<SelectListItem>();
                    AvailableStores = new List<SelectListItem>();
                    AvailableVendors = new List<SelectListItem>();
                    AvailableProductTypes = new List<SelectListItem>();
                }

                [AllowHtml]
                public string SearchProductName { get; set; }

                public int SearchCategoryId { get; set; }
                public int SearchManufacturerId { get; set; }
                public int SearchStoreId { get; set; }
                public int SearchVendorId { get; set; }
                public int SearchProductTypeId { get; set; }
                public IList<SelectListItem> AvailableCategories { get; set; }
                public IList<SelectListItem> AvailableManufacturers { get; set; }
                public IList<SelectListItem> AvailableStores { get; set; }
                public IList<SelectListItem> AvailableVendors { get; set; }
                public IList<SelectListItem> AvailableProductTypes { get; set; }

                //vendor
                public bool IsLoggedInAsVendor { get; set; }
                public int AssociatedToProductId { get; set; }
            }

            #endregion Nested classes
        }

        public class ProductAttributeCombinationModel : BaseEntity
        {
            public int ProductId { get; set; }

            [AllowHtml]
            public string AttributesXml { get; set; }

            [AllowHtml]
            public string Warnings { get; set; }

            public int StockQuantity { get; set; }
            public bool AllowOutOfStockOrders { get; set; }
            public string Sku { get; set; }
            public string ManufacturerPartNumber { get; set; }
            public string Gtin { get; set; }
            public decimal? OverriddenPrice { get; set; }
            public int NotifyAdminForQuantityBelow { get; set; }
        }

        #endregion Nested classes
    }
}