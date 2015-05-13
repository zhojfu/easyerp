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


        public JsonResult CustomerList(int skip, int take, int page, int pageSize)
        {
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
            if (ids != null)
            {
                this.customerService.DeleteCustomerByIds(ids);
            }
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
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
            var e = Mapper.Map<CustomerModel, Customer>(customer);
            if (e != null)
            {
                this.customerService.UpdateCustomer(e);
            }
            return RedirectToAction("Index");
        }
    }
}
