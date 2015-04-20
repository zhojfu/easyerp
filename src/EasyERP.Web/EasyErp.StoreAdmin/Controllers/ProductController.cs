namespace EasyErp.StoreAdmin.Controllers
{
    using Doamin.Service.Products;
    using Doamin.Service.Security;
    using Domain.Model.Products;
    using EasyErp.StoreAdmin.Extensions;
    using EasyErp.StoreAdmin.Models.Products;
    using EasyERP.Web.Framework.Controllers;
    using EasyERP.Web.Framework.Kendoui;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class ProductController : BaseController
    {
        private readonly IPermissionService permissionService;
        private readonly IProductService productService;

        private readonly ICategoryService categoryService;

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
            return this.RedirectToAction("List");
        }

        // List products in the store
        public ActionResult List()
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return this.AccessDeniedView();
            }
            var model = new ProductListModel();

            //categories
            model.AvailableCategories.Add(new SelectListItem { Text = "All", Value = "0" });
            var categories = this.categoryService.GetAllCategories(showHidden: true);
            foreach (var c in categories)
                model.AvailableCategories.Add(new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

            return this.View(model);
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

            var products = this.productService.SearchProducts(
                categoryIds: categoryIds,
                keywords: model.SearchProductName,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize,
                showHidden: true,
                overridePublished: overridePublished
            );

            var gridModel = new DataSourceResult();
            gridModel.Data = products.Select(x =>
            {
                ProductModel productModel = x.ToModel();

                productModel.FullDescription = "";

                return productModel;
            });
            gridModel.Total = products.TotalCount;

            return Json(gridModel);
        }
    }
}