using Infrastructure;

namespace EasyERP.Web.Controllers
{
    using Doamin.Service.Security;
    using Doamin.Service.Stores;
    using EasyERP.Web.Extensions;
    using EasyERP.Web.Framework.Kendoui;
    using EasyERP.Web.Models.Products;
    using EasyERP.Web.Models.Stores;
    using System;
    using System.Linq;
    using System.Web.Mvc;

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
            if (!permissionService.Authorize(StandardPermissionProvider.GetStoreList))
            {
                return AccessDeniedView();
            }
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            if (!permissionService.Authorize(StandardPermissionProvider.GetStoreList))
            {
                return AccessDeniedView();
            }
            storeService.GetAllStores();
            return View();
        }

        [HttpPost]
        public ActionResult StoreList(DataSourceRequest command, ProductListModel model)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.GetStoreList))
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
            if (!permissionService.Authorize(StandardPermissionProvider.CreateStore))
            {
                return AccessDeniedView();
            }
            var model = new StoreModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(StoreModel model)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.CreateStore))
            {
                return AccessDeniedView();
            }

            if (ModelState.IsValid)
            {
                var store = model.ToEntity();
                store.CompanyId = 1;
                store.CreatedOn = DateTime.Now;
                store.UpdatedOn = DateTime.Now;
                storeService.InsertStore(store);
            }

            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.UpdateStore))
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
            if (!permissionService.Authorize(StandardPermissionProvider.UpdateStore))
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
                store.Name = model.Name;
                store.FullDescription = model.FullDescription;
                store.Address = model.Address;
                store.PhoneNumber = model.PhoneNumber;
                store.UpdatedOn = DateTime.Now;
                storeService.UpdateStore(store);
                return RedirectToAction("List");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Destroy(DataSourceRequest request, int id)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.DeleteProduct))
            {
                return AccessDeniedView();
            }

            if (id < 1)
            {
                return Json(new { Result = false });
            }

            var store = storeService.GetStoreById(id);
            store.DoIfNotNull(s => storeService.DeleteStore(s));

            return Json(new { Result = true });
        }
    }
}