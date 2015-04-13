namespace EasyERP.Web.Areas.Factory.Controllers
{
    using Doamin.Service.Products;
    using Doamin.Service.Security;
    using EasyERP.Web.Framework.Controllers;
    using EasyERP.Web.Models.Products;
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
            return this.View();
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

            model.AvailablePublishedOptions.Add(new SelectListItem { Text = "All", Value = "0" });
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = "PublishedOnly", Value = "1" });
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = "UnpublishedOnly", Value = "2" });

            return this.View();
        }
    }
}