namespace EasyERP.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using System.Web.Mvc;
    using Doamin.Service.Customer;
    using Domain.Model.Customer;
    using EasyERP.Web.Models.Customer;

    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CustomerModel customer)
        {
            Customer e = Mapper.Map<CustomerModel, Customer>(customer);
            if (e != null)
            {
                e.CreatedOn = DateTime.Now;
                e.UpdatedOn = DateTime.Now;
                this.customerService.AddCustomer(e);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(List<int> ids)
        {
            if (ids != null)
            {
                this.customerService.DeleteCustomerByIds(ids);
            }
            return Json(null);
        }

        [HttpPost]
        public ActionResult Edit(CustomerModel customer)
        {
            var e = Mapper.Map<CustomerModel, Customer>(customer);
            if (e != null)
            {
                this.customerService.UpdateCustomer(e);
            }
            return RedirectToAction("Index");
        }
    }
}
