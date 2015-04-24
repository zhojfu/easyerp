namespace EasyErp.StoreAdmin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Doamin.Service.Products;
    using Doamin.Service.Security;
    using Domain.Model.Products;
    using EasyErp.StoreAdmin.Extensions;
    using EasyErp.StoreAdmin.Models.Products;
    using EasyERP.Web.Framework.Controllers;
    using EasyERP.Web.Framework.Kendoui;

    public class ProductController : BaseController
    {
        private readonly ICategoryService categoryService;

        private readonly IPermissionService permissionService;

        private readonly IProductService productService;

        public ProductController(
            IPermissionService permissionService,
            IProductService productService,
            ICategoryService categoryService)
        {
            this.permissionService = permissionService;
            this.productService = productService;
            this.categoryService = categoryService;
        }

        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        // List products in the store
        public ActionResult List()
        {
            if (!permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return AccessDeniedView();
            }
            var model = new ProductListModel();

            //categories
            model.AvailableCategories.Add(
                new SelectListItem
                {
                    Text = "All",
                    Value = "0"
                });
            var categories = categoryService.GetAllCategories();
            foreach (var c in categories)
            {
                model.AvailableCategories.Add(
                    new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    });
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult ProductList(DataSourceRequest command, ProductListModel model)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return AccessDeniedView();
            }

            var categoryIds = new List<int>
            {
                model.SearchCategoryId
            };

            var products = productService.SearchProducts(
                categoryIds: categoryIds,
                keywords: model.SearchProductName,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize
                );

            var gridModel = new DataSourceResult();
            gridModel.Data = products.Select(
                x =>
                {
                    var productModel = x.ToModel();

                    productModel.FullDescription = "";

                    return productModel;
                });
            gridModel.Total = products.TotalCount;

            return Json(gridModel);
        }

        public ActionResult Shopping()
        {
            if (!permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return AccessDeniedView();
            }

            var products = productService.GetAllProducts();
            return View(PrepareProductOverviewModels(products));
        }

        private IEnumerable<ProductOverviewModel> PrepareProductOverviewModels(IEnumerable<Product> products)
        {
            if (products == null)
            {
                throw new ArgumentNullException("products");
            }

            var modes = new List<ProductOverviewModel>();

            foreach (var product in products)
            {
                var model = new ProductOverviewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    FullDescription = product.FullDescription,
                    ShortDescription = product.ShortDescription,
                    ProductPrice = new ProductOverviewModel.ProductPriceModel
                    {
                        Price = product.Price
                    }
                };

                modes.Add(model);
            }
            return modes;
        }
    }
}