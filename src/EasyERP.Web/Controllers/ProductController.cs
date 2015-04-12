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
    using Domain.Model.Payment;
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
        private readonly IPermissionService permissionService;
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

        private readonly IInventoryService inventoryService;

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
            IInventoryService inventoryService,
            IAclService aclService)
        {
            this._categoryService = categoryService;
            this.permissionService = permissionService;
            this._productTemplateService = productTemplateService;
            this._productService = productService;
            this._manufacturerService = manufacturerService;
            this._discountService = discountService;
            this._storeService = storeService;
            this.inventoryService = inventoryService;
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

        [HttpPost]
        public ActionResult ProductList(DataSourceRequest command, ProductListModel model)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return AccessDeniedView();
            }

            var categoryIds = new List<int> { model.SearchCategoryId };

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

                productModel.FullDescription = "";

                return productModel;
            });
            gridModel.Total = products.TotalCount;

            return Json(gridModel);
        }

        public ActionResult List()
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return this.AccessDeniedView();
            }
            var model = new ProductListModel();

            //categories
            model.AvailableCategories.Add(new SelectListItem { Text = "All", Value = "0" });
            var categories = _categoryService.GetAllCategories(showHidden: true);
            foreach (var c in categories)
                model.AvailableCategories.Add(new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

            model.AvailablePublishedOptions.Add(new SelectListItem { Text = "All", Value = "0" });
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = "PublishedOnly", Value = "1" });
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = "UnpublishedOnly", Value = "2" });

            return this.View(model);
        }

        public ActionResult Inventory()
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return this.AccessDeniedView();
            }

            var model = new InventoryModel();

            var allCategories = _categoryService.GetAllCategories(showHidden: true);
            foreach (var category in allCategories)
            {
                model.AvailableCategories.Add(new SelectListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                });
            }

            var products = _productService.GetAllProducts();

            if (products == null ||
                !products.Any())
            {
                return this.RedirectToAction("Create");
            }

            products.ToList().ForEach(
                p =>
                {
                    model.AvailableProducts.Add(new SelectListItem { Text = p.Name, Value = p.Id.ToString() });
                });

            model.Paid = 0;
            model.DueDateTime = DateTime.Now + TimeSpan.FromDays(30);

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Inventory(InventoryModel model)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return this.AccessDeniedView();
            }
            if (this.ModelState.IsValid)
            {
                var inventory = model.ToEntity();
                inventory.InStockTime = DateTime.Now;
                var payment = new Payment
                {
                    DueDateTime = model.DueDateTime,
                    Paid = model.Paid,
                    Payables = model.Payables
                };

                this.inventoryService.InsertInventory(inventory, payment);
            }

            return this.RedirectToAction("List");
        }

        //create product
        public ActionResult Create()
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageProducts))
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
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return this.AccessDeniedView();
            }

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
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageProducts))
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
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageProducts))
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

        [NonAction]
        protected virtual void PrepareProductModel(
            ProductModel model,
            Product product,
            bool setPredefinedValues,
            bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            var allCategories = _categoryService.GetAllCategories(showHidden: true);
            foreach (var category in allCategories)
            {
                model.AvailableCategories.Add(new SelectListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                });
            }

            if (product != null)
            {
                model.CreatedOn = _dateTimeHelper.ConvertToUserTime(product.CreatedOnUtc, DateTimeKind.Utc);
                model.UpdatedOn = _dateTimeHelper.ConvertToUserTime(product.UpdatedOnUtc, DateTimeKind.Utc);
            }

            //default values
            if (setPredefinedValues)
            {
                model.StockQuantity = 10000;

                model.Published = true;
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageProducts))
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
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageProducts))
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