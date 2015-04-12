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
    using EasyERP.Web.Models.Stores;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class StoreController : BaseAdminController
    {
        private readonly IStoreService storeService;

        private readonly IPermissionService permissionService;

        public StoreController(
            IPermissionService permissionService,
            IStoreService storeService)
        {
            this.storeService = storeService;
            this.permissionService = permissionService;
        }

        public ActionResult Index()
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageStores))
            {
                return this.AccessDeniedView();
            }
            return this.RedirectToAction("List");
        }

        public ActionResult List()
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageStores))
            {
                return this.AccessDeniedView();
            }
            this.storeService.GetAllStores();
            return this.View();
        }

        [HttpPost]
        public ActionResult StoreList(DataSourceRequest command, ProductListModel model)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return AccessDeniedView();
            }

            var stores = this.storeService.GetAllStores();

            var gridModel = new DataSourceResult();
            gridModel.Data = stores.Select(x => x.ToModel());
            gridModel.Total = stores.Count;

            return Json(gridModel);
        }

        public ActionResult Create()
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageStores))
            {
                return this.AccessDeniedView();
            }
            var model = new StoreModel();
            return this.View(model);
        }

        [HttpPost]
        public ActionResult Create(StoreModel model)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageStores))
            {
                return this.AccessDeniedView();
            }

            if (this.ModelState.IsValid)
            {
                var store = model.ToEntity();
                store.CreatedOn = DateTime.Now;
                store.UpdatedOn = DateTime.Now;
                this.storeService.InsertStore(store);
            }

            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageStores))
            {
                return this.AccessDeniedView();
            }
            var store = this.storeService.GetStoreById(id);

            if (store == null)
            {
                return this.RedirectToAction("List");
            }
            var model = store.ToModel();
            return this.View(model);
        }

        [HttpPost]
        public ActionResult Edit(StoreModel model)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.ManageStores))
            {
                return this.AccessDeniedView();
            }

            var store = this.storeService.GetStoreById(model.Id);

            if (store == null)
            {
                return this.RedirectToAction("List");
            }

            if (this.ModelState.IsValid)
            {
                store = model.ToEntity();
                store.UpdatedOn = DateTime.Now;
                this.storeService.UpdateStore(store);
                return this.RedirectToAction("List");
            }
            return this.View(model);
        }
    }
}