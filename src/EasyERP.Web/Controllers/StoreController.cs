namespace EasyERP.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Doamin.Service.Security;
    using Doamin.Service.Stores;
    using EasyERP.Web.Extensions;
    using EasyERP.Web.Framework.Kendoui;
    using EasyERP.Web.Models.Products;
    using EasyERP.Web.Models.Stores;

    public class StoreController : BaseAdminController
    {
        private readonly IPermissionService permissionService;

        private readonly IStoreService storeService;

        public StoreController(
            IPermissionService permissionService,
            IStoreService storeService)
        {
            this.storeService = storeService;
            this.permissionService = permissionService;
        }

        public ActionResult Index()
        {
            if (!permissionService.Authorize(StandardPermissionProvider.ManageStores))
            {
                return AccessDeniedView();
            }
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            if (!permissionService.Authorize(StandardPermissionProvider.ManageStores))
            {
                return AccessDeniedView();
            }
            storeService.GetAllStores();
            return View();
        }

        [HttpPost]
        public ActionResult StoreList(DataSourceRequest command, ProductListModel model)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return AccessDeniedView();
            }

            var stores = storeService.GetAllStores();

            var gridModel = new DataSourceResult
            {
                Data = stores.Select(x => x.ToModel()),
                Total = stores.Count
            };

            return Json(gridModel);
        }

        public ActionResult Create()
        {
            if (!permissionService.Authorize(StandardPermissionProvider.ManageStores))
            {
                return AccessDeniedView();
            }
            var model = new StoreModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(StoreModel model)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.ManageStores))
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var store = model.ToEntity();
                store.CreatedOn = DateTime.Now;
                store.UpdatedOn = DateTime.Now;
                storeService.InsertStore(store);
            }

            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.ManageStores))
            {
                return AccessDeniedView();
            }
            var store = storeService.GetStoreById(id);

            if (store == null)
            {
                return RedirectToAction("List");
            }
            var model = store.ToModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(StoreModel model)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.ManageStores))
            {
                return AccessDeniedView();
            }

            var store = storeService.GetStoreById(model.Id);

            if (store == null)
            {
                return RedirectToAction("List");
            }

            if (ModelState.IsValid)
            {
                store = model.ToEntity();
                store.UpdatedOn = DateTime.Now;
                storeService.UpdateStore(store);
                return RedirectToAction("List");
            }
            return View(model);
        }
    }
}