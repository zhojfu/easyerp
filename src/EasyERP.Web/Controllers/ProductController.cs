namespace EasyERP.Web.Controllers
{
    using Doamin.Service.Directory;
    using Doamin.Service.Discounts;
    using Doamin.Service.Helpers;
    using Doamin.Service.Products;
    using Doamin.Service.Security;
    using Doamin.Service.Stores;
    using Doamin.Service.Vendors;
    using Domain.Model.Directory;
    using Domain.Model.Discounts;
    using Domain.Model.Products;
    using Domain.Model.Vendors;
    using EasyErp.Core;
    using EasyERP.Web.Extensions;
    using EasyERP.Web.Kendoui;
    using EasyERP.Web.Models.Products;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class ProductController : BaseAdminController
    {
        private readonly IPermissionService _permissionService;
        private readonly IProductTemplateService _productTemplateService;

        //private readonly IWorkContext _workContext;
        private readonly IProductService _productService;

        private readonly ICategoryService _categoryService;

        private readonly IDiscountService _discountService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IStoreService _storeService;
        private readonly IMeasureService _measureService;
        private readonly MeasureSettings _measureSettings;
        private readonly IVendorService _vendorService;
        private readonly ISpecificationAttributeService _specificationAttributeService;

        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IAclService aclService;

        public ProductController(
            IPermissionService permissionService,
            ICategoryService categoryService,
            IProductTemplateService productTemplateService,
            IProductService productService,
            IManufacturerService manufacturerService,
            IStoreService storeService,
            IMeasureService measureService,
            IDateTimeHelper dateTimeHelper,
            IProductAttributeService productAttributeService,
            ISpecificationAttributeService specificationAttributeService,
            IDiscountService discountService,
            IVendorService vendorService,
            IAclService aclService)
        {
            this._categoryService = categoryService;
            this._permissionService = permissionService;
            this._productTemplateService = productTemplateService;
            this._productService = productService;
            this._manufacturerService = manufacturerService;
            this._discountService = discountService;
            this._storeService = storeService;
            this._measureService = measureService;
            this._measureSettings = new MeasureSettings();
            this._dateTimeHelper = dateTimeHelper;
            this._vendorService = vendorService;
            this._productAttributeService = productAttributeService;
            this.aclService = aclService;
            this._specificationAttributeService = specificationAttributeService;
        }

        // GET: Product
        public ActionResult Index()
        {
            return this.RedirectToAction("List");
        }

        public ActionResult List()
        {
            if (!this._permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return this.AccessDeniedView();
            }
            var model = new ProductListModel();

            //categories
            model.AvailableCategories.Add(new SelectListItem { Text = "All", Value = "0" });
            var categories = _categoryService.GetAllCategories(showHidden: true);
            foreach (var c in categories)
                model.AvailableCategories.Add(new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

            ////manufacturers
            //model.AvailableManufacturers.Add(new SelectListItem { Text = "All", Value = "0" });
            //foreach (var m in _manufacturerService.GetAllManufacturers(showHidden: true))
            //    model.AvailableManufacturers.Add(new SelectListItem { Text = m.Name, Value = m.Id.ToString() });

            ////stores
            //model.AvailableStores.Add(new SelectListItem { Text = "All", Value = "0" });
            //foreach (var s in _storeService.GetAllStores())
            //    model.AvailableStores.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString() });

            //"published" property
            //0 - all (according to "ShowHidden" parameter)
            //1 - published only
            //2 - unpublished only
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = "All", Value = "0" });
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = "PublishedOnly", Value = "1" });
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = "UnpublishedOnly", Value = "2" });

            return this.View(model);
        }

        [HttpPost]
        public ActionResult ProductList(DataSourceRequest command, ProductListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return AccessDeniedView();
            }

            var categoryIds = new List<int> { model.SearchCategoryId };
            ////include subcategories
            //if (model.SearchIncludeSubCategories && model.SearchCategoryId > 0)
            //    categoryIds.AddRange(GetChildCategoryIds(model.SearchCategoryId));

            //0 - all (according to "ShowHidden" parameter)
            //1 - published only
            //2 - unpublished only
            bool? overridePublished = null;
            if (model.SearchPublishedId == 1)
                overridePublished = true;
            else if (model.SearchPublishedId == 2)
                overridePublished = false;

            var products = _productService.SearchProducts(
                categoryIds: categoryIds,
                manufacturerId: model.SearchManufacturerId,
                storeId: model.SearchStoreId,
                vendorId: model.SearchVendorId,
                warehouseId: model.SearchWarehouseId,
                keywords: model.SearchProductName,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize,
                showHidden: true,
                overridePublished: overridePublished
            );
            var gridModel = new DataSourceResult();
            gridModel.Data = products.Select(x =>
            {
                var productModel = x.ToModel();
                //little hack here:
                //ensure that product full descriptions are not returned
                //otherwise, we can get the following error if products have too long descriptions:
                //"Error during serialization or deserialization using the JSON JavaScriptSerializer. The length of the string exceeds the value set on the maxJsonLength property. "
                //also it improves performance
                productModel.FullDescription = "";

                //if (_adminAreaSettings.DisplayProductPictures)
                //{
                //    var defaultProductPicture = _pictureService.GetPicturesByProductId(x.Id, 1).FirstOrDefault();
                //    productModel.PictureThumbnailUrl = _pictureService.GetPictureUrl(defaultProductPicture, 75, true);
                //}
                //productModel.ProductTypeName = x.ProductType.GetLocalizedEnum(_localizationService, _workContext);
                return productModel;
            });
            gridModel.Total = products.TotalCount;

            return Json(gridModel);
        }

        //create product
        public ActionResult Create()
        {
            if (!this._permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return this.AccessDeniedView();
            }

            var model = new ProductModel();
            this.PrepareProductModel(model, null, true, true);
            this.PrepareAclModel(model, null, false);
            this.PrepareStoresMappingModel(model, null, false);
            return this.View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(ProductModel model, bool continueEditing)
        {
            if (!this._permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return this.AccessDeniedView();
            }

            model.VendorId = 0;

            if (this.ModelState.IsValid)
            {
                //product
                var product = model.ToEntity();
                product.CreatedOnUtc = DateTime.UtcNow;
                product.UpdatedOnUtc = DateTime.UtcNow;

                this._productService.InsertProduct(product);

                return continueEditing
                           ? this.RedirectToAction(
                               "Edit",
                               new
                               {
                                   id = product.Id
                               })
                           : this.RedirectToAction("List");
            }

            //If we got this far, something failed, redisplay form
            this.PrepareProductModel(model, null, false, true);
            this.PrepareAclModel(model, null, true);
            this.PrepareStoresMappingModel(model, null, true);
            return this.View(model);
        }

        public ActionResult Edit(int id)
        {
            if (!this._permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return this.AccessDeniedView();
            }

            var product = this._productService.GetProductById(id);

            //No product found with the specified id
            if (product == null ||
                product.Deleted)
            {
                return this.RedirectToAction("List");
            }

            var model = product.ToModel();
            this.PrepareProductModel(model, product, false, false);
            this.PrepareAclModel(model, product, false);
            this.PrepareStoresMappingModel(model, product, false);
            return this.View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(ProductModel model, bool continueEditing)
        {
            if (!this._permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return this.AccessDeniedView();
            }

            var product = this._productService.GetProductById(model.Id);
            if (product == null ||
                product.Deleted)
            {
                //No product found with the specified id
                return this.RedirectToAction("List");
            }

            if (this.ModelState.IsValid)
            {
                //product
                product = model.ToEntity(product);
                this._productService.UpdateProduct(product);

                //discounts

                this._productService.UpdateProduct(product);

                if (continueEditing)
                {
                    return this.RedirectToAction(
                        "Edit",
                        new
                        {
                            id = product.Id
                        });
                }
                return this.RedirectToAction("List");
            }

            //If we got this far, something failed, redisplay form
            this.PrepareProductModel(model, product, false, true);
            this.PrepareAclModel(model, product, true);
            this.PrepareStoresMappingModel(model, product, true);
            return this.View(model);
        }

        //[HttpPost]
        //public ActionResult ProductCategoryList(DataSourceRequest command, int productId)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
        //        return AccessDeniedView();

        //    //a vendor should have access only to his products
        //    if (_workContext.CurrentVendor != null)
        //    {
        //        var product = _productService.GetProductById(productId);
        //        if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
        //        {
        //            return Content("This is not your product");
        //        }
        //    }

        //    var productCategories = _categoryService.GetProductCategoriesByProductId(productId, true);
        //    var productCategoriesModel = productCategories
        //        .Select(x => new ProductModel.ProductCategoryModel
        //        {
        //            Id = x.Id,
        //            Category = _categoryService.GetCategoryById(x.CategoryId).GetFormattedBreadCrumb(_categoryService),
        //            ProductId = x.ProductId,
        //            CategoryId = x.CategoryId,
        //            IsFeaturedProduct = x.IsFeaturedProduct,
        //            DisplayOrder = x.DisplayOrder
        //        })
        //        .ToList();

        //    var gridModel = new DataSourceResult
        //    {
        //        Data = productCategoriesModel,
        //        Total = productCategoriesModel.Count
        //    };

        //    return Json(gridModel);
        //}

        [NonAction]
        protected virtual void PrepareProductModel(
            ProductModel model,
            Product product,
            bool setPredefinedValues,
            bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            //if (product != null)
            //{
            //    var parentGroupedProduct = _productService.GetProductById(product.ParentGroupedProductId);
            //    if (parentGroupedProduct != null)
            //    {
            //        model.AssociatedToProductId = product.ParentGroupedProductId;
            //        model.AssociatedToProductName = parentGroupedProduct.Name;
            //    }
            //}

            //model.PrimaryStoreCurrencyCode = _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId).CurrencyCode;
            //model.BaseWeightIn = _measureService.GetMeasureWeightById(_measureSettings.BaseWeightId).Name;
            //model.BaseDimensionIn = _measureService.GetMeasureDimensionById(_measureSettings.BaseDimensionId).Name;
            if (product != null)
            {
                model.CreatedOn = _dateTimeHelper.ConvertToUserTime(product.CreatedOnUtc, DateTimeKind.Utc);
                model.UpdatedOn = _dateTimeHelper.ConvertToUserTime(product.UpdatedOnUtc, DateTimeKind.Utc);
            }

            //little performance hack here
            //there's no need to load attributes, categories, manufacturers when creating a new product
            //anyway they're not used (you need to save a product before you map add them)
            if (product != null)
            {
                //foreach (var productAttribute in _productAttributeService.GetAllProductAttributes())
                //{
                //    model.AvailableProductAttributes.Add(new SelectListItem
                //    {
                //        Text = productAttribute.Name,
                //        Value = productAttribute.Id.ToString()
                //    });
                //}
                //foreach (var manufacturer in _manufacturerService.GetAllManufacturers(showHidden: true))
                //{
                //    model.AvailableManufacturers.Add(new SelectListItem
                //    {
                //        Text = manufacturer.Name,
                //        Value = manufacturer.Id.ToString()
                //    });
                //}
                //var allCategories = _categoryService.GetAllCategories(showHidden: true);
                //foreach (var category in allCategories)
                //{
                //    model.AvailableCategories.Add(new SelectListItem
                //    {
                //        Text = category.Name,
                //        Value = category.Id.ToString()
                //    });
                //}
            }

            //templates
            //var templates = _productTemplateService.GetAllProductTemplates();
            //foreach (var template in templates)
            //{
            //    model.AvailableProductTemplates.Add(new SelectListItem
            //    {
            //        Text = template.Name,
            //        Value = template.Id.ToString()
            //    });
            //}

            //vendors
            ////model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;
            //model.AvailableVendors.Add(new SelectListItem
            //{
            //    Text = "None",
            //    Value = "0"
            //});
            //var vendors = _vendorService.GetAllVendors(showHidden: true);
            //foreach (var vendor in vendors)
            //{
            //    //model.AvailableVendors.Add(new SelectListItem
            //    //{
            //    //    Text = vendor.Name,
            //    //    Value = vendor.Id.ToString()
            //    //});
            //}

            ////specification attributes
            //var specificationAttributes = _specificationAttributeService.GetSpecificationAttributes();
            //for (int i = 0; i < specificationAttributes.Count; i++)
            //{
            //    var sa = specificationAttributes[i];
            //    model.AddSpecificationAttributeModel.AvailableAttributes.Add(new SelectListItem { Text = sa.Name, Value = sa.Id.ToString() });
            //    if (i == 0)
            //    {
            //        //attribute options
            //        foreach (var sao in _specificationAttributeService.GetSpecificationAttributeOptionsBySpecificationAttribute((int)sa.Id))
            //            model.AddSpecificationAttributeModel.AvailableOptions.Add(new SelectListItem { Text = sao.Name, Value = sao.Id.ToString() });
            //    }
            //}
            ////default specs values
            //model.AddSpecificationAttributeModel.ShowOnProductPage = true;

            //discounts
            //model.AvailableDiscounts = _discountService
            //    .GetAllDiscounts(DiscountType.AssignedToSkus, null, true)
            //    .Select(d => d.ToModel())
            //    .ToList();
            //if (!excludeProperties && product != null)
            //{
            //    model.SelectedDiscountIds = product.AppliedDiscounts.Select(d => d.Id).ToArray();
            //}

            //default values
            if (setPredefinedValues)
            {
                model.MaximumCustomerEnteredPrice = 1000;
                ;
                model.StockQuantity = 10000;
                model.NotifyAdminForQuantityBelow = 1;
                model.OrderMinimumQuantity = 1;
                model.OrderMaximumQuantity = 10000;

                model.IsShipEnabled = true;
                model.Published = true;
                model.VisibleIndividually = true;
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!this._permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return this.AccessDeniedView();
            }

            var product = this._productService.GetProductById(id);
            if (product == null)
            {
                //No product found with the specified id
                return this.RedirectToAction("List");
            }

            this._productService.DeleteProduct(product);

            return this.RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult DeleteSelected(ICollection<int> selectedIds)
        {
            if (!this._permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return this.AccessDeniedView();
            }

            var products = new List<Product>();
            if (selectedIds != null)
            {
                products.AddRange(this._productService.GetProductsByIds(selectedIds.ToArray()));

                for (var i = 0; i < products.Count; i++)
                {
                    var product = products[i];

                    this._productService.DeleteProduct(product);
                }
            }

            return this.Json(
                new
                {
                    Result = true
                });
        }

        [NonAction]
        protected virtual void PrepareAclModel(ProductModel model, Product product, bool excludeProperties)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
        }

        [NonAction]
        protected virtual void PrepareStoresMappingModel(ProductModel model, Product product, bool excludeProperties)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
        }
    }
}