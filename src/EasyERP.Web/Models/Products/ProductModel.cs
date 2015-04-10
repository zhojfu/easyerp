namespace EasyERP.Web.Models.Products
{
    using EasyERP.Web.Models.Discounts;
    using EasyERP.Web.Models.Stores;
    using EasyERP.Web.Validators.Products;
    using FluentValidation.Attributes;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    [Validator(typeof(ProductValidator))]
    public partial class ProductModel : BaseEntity
    {
        public ProductModel()
        {
            this.ProductPictureModels = new List<ProductPictureModel>();
            this.AvailableProductTemplates = new List<SelectListItem>();
            this.AvailableVendors = new List<SelectListItem>();
            this.AvailableDeliveryDates = new List<SelectListItem>();
            this.AvailableWarehouses = new List<SelectListItem>();
            this.AvailableCategories = new List<SelectListItem>();
            this.AvailableManufacturers = new List<SelectListItem>();
            this.AvailableProductAttributes = new List<SelectListItem>();
            this.AddPictureModel = new ProductPictureModel();
            this.AddSpecificationAttributeModel = new AddProductSpecificationAttributeModel();
            this.ProductWarehouseInventoryModels = new List<ProductWarehouseInventoryModel>();
        }

        public override int Id { get; set; }

        //picture thumbnail
        public string PictureThumbnailUrl { get; set; }

        public int ProductTypeId { get; set; }

        public string ProductTypeName { get; set; }

        public int AssociatedToProductId { get; set; }

        public string AssociatedToProductName { get; set; }

        public bool VisibleIndividually { get; set; }

        public int ProductTemplateId { get; set; }

        public IList<SelectListItem> AvailableProductTemplates { get; set; }

        [AllowHtml]
        public string Name { get; set; }

        [AllowHtml]
        public string ShortDescription { get; set; }

        [AllowHtml]
        public string FullDescription { get; set; }

        [AllowHtml]
        public string AdminComment { get; set; }

        public int VendorId { get; set; }

        public IList<SelectListItem> AvailableVendors { get; set; }

        public bool ShowOnHomePage { get; set; }

        [AllowHtml]
        public string Sku { get; set; }

        [AllowHtml]
        public string ManufacturerPartNumber { get; set; }

        [AllowHtml]
        public virtual string Gtin { get; set; }

        public bool IsDownload { get; set; }

        public bool HasUserAgreement { get; set; }

        [AllowHtml]
        public string UserAgreementText { get; set; }

        public bool IsShipEnabled { get; set; }

        public bool IsFreeShipping { get; set; }

        public bool ShipSeparately { get; set; }

        public decimal AdditionalShippingCharge { get; set; }

        public int DeliveryDateId { get; set; }

        public IList<SelectListItem> AvailableDeliveryDates { get; set; }

        public int ManageInventoryMethodId { get; set; }

        public bool UseMultipleWarehouses { get; set; }

        public int WarehouseId { get; set; }

        public IList<SelectListItem> AvailableWarehouses { get; set; }

        public int StockQuantity { get; set; }

        public bool DisplayStockAvailability { get; set; }

        public bool DisplayStockQuantity { get; set; }

        public int MinStockQuantity { get; set; }

        public int LowStockActivityId { get; set; }

        public int NotifyAdminForQuantityBelow { get; set; }

        public bool AllowBackInStockSubscriptions { get; set; }

        public int OrderMinimumQuantity { get; set; }

        public int OrderMaximumQuantity { get; set; }

        public string AllowedQuantities { get; set; }

        public bool AllowAddingOnlyExistingAttributeCombinations { get; set; }

        public bool DisableBuyButton { get; set; }

        public bool DisableWishlistButton { get; set; }

        public bool AvailableForPreOrder { get; set; }

        [UIHint("DateTimeNullable")]
        public DateTime? PreOrderAvailabilityStartDateTimeUtc { get; set; }

        public bool CallForPrice { get; set; }

        public decimal Price { get; set; }

        public decimal OldPrice { get; set; }

        public decimal ProductCost { get; set; }

        [UIHint("DecimalNullable")]
        public decimal? SpecialPrice { get; set; }

        [UIHint("DateTimeNullable")]
        public DateTime? SpecialPriceStartDateTimeUtc { get; set; }

        [UIHint("DateTimeNullable")]
        public DateTime? SpecialPriceEndDateTimeUtc { get; set; }

        public bool CustomerEntersPrice { get; set; }

        public decimal MinimumCustomerEnteredPrice { get; set; }

        public decimal MaximumCustomerEnteredPrice { get; set; }

        public decimal Weight { get; set; }

        public decimal Length { get; set; }

        public decimal Width { get; set; }

        public decimal Height { get; set; }

        [UIHint("DateTimeNullable")]
        public DateTime? AvailableStartDateTimeUtc { get; set; }

        [UIHint("DateTimeNullable")]
        public DateTime? AvailableEndDateTimeUtc { get; set; }

        public int DisplayOrder { get; set; }

        public bool Published { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string PrimaryStoreCurrencyCode { get; set; }

        public string BaseDimensionIn { get; set; }

        public string BaseWeightIn { get; set; }

        //ACL (customer roles)

        public bool SubjectToAcl { get; set; }

        //public List<CustomerRoleModel> AvailableCustomerRoles { get; set; }

        public int[] SelectedCustomerRoleIds { get; set; }

        //Store mapping

        public bool LimitedToStores { get; set; }

        public List<StoreModel> AvailableStores { get; set; }

        public int[] SelectedStoreIds { get; set; }

        //vendor
        public bool IsLoggedInAsVendor { get; set; }

        //categories
        public IList<SelectListItem> AvailableCategories { get; set; }

        //manufacturers
        public IList<SelectListItem> AvailableManufacturers { get; set; }

        //product attributes
        public IList<SelectListItem> AvailableProductAttributes { get; set; }

        //pictures
        public ProductPictureModel AddPictureModel { get; set; }

        public IList<ProductPictureModel> ProductPictureModels { get; set; }

        //discounts
        public List<DiscountModel> AvailableDiscounts { get; set; }

        public int[] SelectedDiscountIds { get; set; }

        //add specification attribute model
        public AddProductSpecificationAttributeModel AddSpecificationAttributeModel { get; set; }

        //multiple warehouses

        public IList<ProductWarehouseInventoryModel> ProductWarehouseInventoryModels { get; set; }

        #region Nested classes

        public partial class AddRequiredProductModel : BaseEntity
        {
            public AddRequiredProductModel()
            {
                this.AvailableCategories = new List<SelectListItem>();
                this.AvailableManufacturers = new List<SelectListItem>();
                this.AvailableStores = new List<SelectListItem>();
                this.AvailableVendors = new List<SelectListItem>();
                this.AvailableProductTypes = new List<SelectListItem>();
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

        public partial class AddProductSpecificationAttributeModel : BaseEntity
        {
            public AddProductSpecificationAttributeModel()
            {
                this.AvailableAttributes = new List<SelectListItem>();
                this.AvailableOptions = new List<SelectListItem>();
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

        public partial class ProductPictureModel : BaseEntity
        {
            public int ProductId { get; set; }

            [UIHint("Picture")]
            public int PictureId { get; set; }

            public string PictureUrl { get; set; }

            public int DisplayOrder { get; set; }
        }

        public partial class ProductCategoryModel : BaseEntity
        {
            public string Category { get; set; }

            public int ProductId { get; set; }

            public int CategoryId { get; set; }

            public bool IsFeaturedProduct { get; set; }

            public int DisplayOrder { get; set; }
        }

        public partial class ProductManufacturerModel : BaseEntity
        {
            public string Manufacturer { get; set; }

            public int ProductId { get; set; }

            public int ManufacturerId { get; set; }

            public bool IsFeaturedProduct { get; set; }

            public int DisplayOrder { get; set; }
        }

        public partial class RelatedProductModel : BaseEntity
        {
            public int ProductId2 { get; set; }

            public string Product2Name { get; set; }

            public int DisplayOrder { get; set; }
        }

        public partial class AddRelatedProductModel : BaseEntity
        {
            public AddRelatedProductModel()
            {
                this.AvailableCategories = new List<SelectListItem>();
                this.AvailableManufacturers = new List<SelectListItem>();
                this.AvailableStores = new List<SelectListItem>();
                this.AvailableVendors = new List<SelectListItem>();
                this.AvailableProductTypes = new List<SelectListItem>();
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

        public partial class AssociatedProductModel : BaseEntity
        {
            public string ProductName { get; set; }

            public int DisplayOrder { get; set; }
        }

        public partial class AddAssociatedProductModel : BaseEntity
        {
            public AddAssociatedProductModel()
            {
                this.AvailableCategories = new List<SelectListItem>();
                this.AvailableManufacturers = new List<SelectListItem>();
                this.AvailableStores = new List<SelectListItem>();
                this.AvailableVendors = new List<SelectListItem>();
                this.AvailableProductTypes = new List<SelectListItem>();
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

        public partial class CrossSellProductModel : BaseEntity
        {
            public int ProductId2 { get; set; }

            public string Product2Name { get; set; }
        }

        public partial class AddCrossSellProductModel : BaseEntity
        {
            public AddCrossSellProductModel()
            {
                this.AvailableCategories = new List<SelectListItem>();
                this.AvailableManufacturers = new List<SelectListItem>();
                this.AvailableStores = new List<SelectListItem>();
                this.AvailableVendors = new List<SelectListItem>();
                this.AvailableProductTypes = new List<SelectListItem>();
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

        public partial class TierPriceModel : BaseEntity
        {
            public int ProductId { get; set; }

            public int CustomerRoleId { get; set; }

            public string CustomerRole { get; set; }

            public int StoreId { get; set; }

            public string Store { get; set; }

            public int Quantity { get; set; }

            public decimal Price { get; set; }
        }

        public partial class ProductWarehouseInventoryModel : BaseEntity
        {
            public int WarehouseId { get; set; }

            public string WarehouseName { get; set; }

            public bool WarehouseUsed { get; set; }

            public int StockQuantity { get; set; }

            public int ReservedQuantity { get; set; }

            public int PlannedQuantity { get; set; }
        }

        public partial class ProductAttributeMappingModel : BaseEntity
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

            [UIHint("Int32Nullable")]
            public int? ValidationMinLength { get; set; }

            [UIHint("Int32Nullable")]
            public int? ValidationMaxLength { get; set; }

            public string ValidationFileAllowedExtensions { get; set; }

            [UIHint("Int32Nullable")]
            public int? ValidationFileMaximumSize { get; set; }

            public string DefaultValue { get; set; }
        }

        public partial class ProductAttributeValueListModel : BaseEntity
        {
            public int ProductId { get; set; }

            public string ProductName { get; set; }

            public int ProductAttributeMappingId { get; set; }

            public string ProductAttributeName { get; set; }
        }

        [Validator(typeof(ProductAttributeValueModelValidator))]
        public partial class ProductAttributeValueModel : BaseEntity
        {
            public ProductAttributeValueModel()
            {
                this.ProductPictureModels = new List<ProductPictureModel>();
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

            public partial class AssociateProductToAttributeValueModel : BaseEntity
            {
                public AssociateProductToAttributeValueModel()
                {
                    this.AvailableCategories = new List<SelectListItem>();
                    this.AvailableManufacturers = new List<SelectListItem>();
                    this.AvailableStores = new List<SelectListItem>();
                    this.AvailableVendors = new List<SelectListItem>();
                    this.AvailableProductTypes = new List<SelectListItem>();
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

        public partial class ProductAttributeCombinationModel : BaseEntity
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

            [UIHint("DecimalNullable")]
            public decimal? OverriddenPrice { get; set; }

            public int NotifyAdminForQuantityBelow { get; set; }
        }

        #endregion Nested classes
    }
}