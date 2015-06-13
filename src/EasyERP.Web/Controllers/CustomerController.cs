using EasyErp.Core;

namespace EasyERP.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using System.Web.Mvc;
    using Doamin.Service.Customer;
    using Domain.Model.Customer;
    using EasyERP.Web.Models.Customer;
    using Infrastructure.Utility;
    using Doamin.Service.Security;

    public class CustomerController : BaseAdminController
    {
        private readonly ICustomerService customerService;
        private readonly IPermissionService permissionService;
        private readonly IWorkContext workContext;

        public CustomerController(ICustomerService customerService, IPermissionService permissionService,
            IWorkContext workContext)
        {
            this.customerService = customerService;
            this.permissionService = permissionService;
            this.workContext = workContext;
        }

        public ActionResult Index()
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.CreateCustomer))
            {
                return AccessDeniedView();
            }
            return View();
        }

        public ActionResult Create()
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.CreateCustomer))
            {
                return AccessDeniedView();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(CustomerModel customer)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.CreateCustomer))
            {
                return AccessDeniedView();
            }

            Customer e = Mapper.Map<CustomerModel, Customer>(customer);
            if (e != null)
            {
                e.CreatedOn = DateTime.Now;
                e.UpdatedOn = DateTime.Now;
                e.StoreId = workContext.CurrentUser.StoreId;
                this.customerService.AddCustomer(e);
            }

            return RedirectToAction("Index");
        }

        public JsonResult CustomerList(int skip, int take, int page, int pageSize)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.GetCustomerList))
            {
                return AccessDeniedJson();
            }

            PagedResult<Customer> cutomers = this.customerService.GetCustomers(page, pageSize);
            if (cutomers != null)
            {
                List<CustomerListModel> customersList = new List<CustomerListModel>();

                foreach (var customer in cutomers)
                {
                    CustomerListModel model = Mapper.Map<Customer, CustomerListModel>(customer);
                    model.Sex = customer.Male ? "男" : "女";
                    model.CreatedOn = customer.CreatedOn.ToShortDateString();
                    customersList.Add(model);
                }

                return Json(
                    new
                    {
                        total = cutomers.TotalRecords,
                        data = customersList
                    },
                    JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }

        public ActionResult Delete(List<int> ids)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.DeleteCustomer))
            {
                return AccessDeniedView();
            }

            if (ids != null)
            {
                this.customerService.DeleteCustomerByIds(ids);
            }
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.UpdateCustomer))
            {
                return AccessDeniedView();
            }

            Customer c = this.customerService.GetCustomerById(id);
            if (c != null)
            {
                var model = Mapper.Map<Customer, CustomerModel>(c);
                return View(model);
            }
            return View();
        }


        [HttpPost]
        public ActionResult Edit(CustomerModel customer)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.UpdateCustomer))
            {
                return AccessDeniedView();
            }

            var e = Mapper.Map<CustomerModel, Customer>(customer);
            if (e != null)
            {
                this.customerService.UpdateCustomer(e);
            }
            return RedirectToAction("Index");
        }
    }
}
