using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Doamin.Service.Security;
using Doamin.Service.Stores;
using Domain.Model.Stores;

namespace EasyERP.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Doamin.Service.Customer;
    using Doamin.Service.Products;
    using Doamin.Service.StoreSale;
    using Domain.Model.Orders;
    using Domain.Model.Payments;
    using EasyErp.Core;
    using EasyERP.Web.Models.StoreSale;

    public class StoreSaleController : BaseAdminController 
    {
        private readonly ICustomerService customerService;

        private readonly IProductService productService;

        private readonly IStoreSaleService storeSaleService;

        private readonly IPostRetailService retailService;

        private readonly IPermissionService permissionService;

        private readonly IWorkContext workContext;

        public StoreSaleController(
            IPostRetailService retailService,
            IStoreSaleService storeSaleService,
            IProductService productService,
            ICustomerService customerService,
            IWorkContext workContext,
            IPermissionService permissionService)
        {
            this.storeSaleService = storeSaleService;
            this.productService = productService;
            this.customerService = customerService;
            this.retailService = retailService;
            this.permissionService = permissionService;
            this.workContext = workContext;
        }

        // GET: /StoreSales/
        public ActionResult Index()
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.GetStoreSalesRecord))
            {
                return AccessDeniedView();
            }

            return View();
        }

        public ActionResult Create()
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.CreateStoreSalesRecord))
            {
                return AccessDeniedView();
            }

            return View();
        }

        [HttpPost]
        public JsonResult AddOrderItem(OrderItemModel orderItem)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.CreateStoreSalesRecord))
            {
                return AccessDeniedJson();
            }

            return Json(orderItem);
        }

        [HttpPost]
        public JsonResult DeleteOrderItem(OrderItemModel orderItem)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.DeleteStoreSalesRecord))
            {
                return AccessDeniedJson();
            }

            return Json(orderItem);
        }

        [HttpPost]
        public JsonResult UpdateOrderItem(OrderItemModel orderItem)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.UpdateStoreSalesRecord))
            {
                return AccessDeniedJson();
            }

            return Json(orderItem);
        }

        public ActionResult Edit(int id)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.UpdateStoreSalesRecord))
            {
                return AccessDeniedView();
            }

            OrderModel model = new OrderModel();
            model.OrderId = id;

            return View(model);
        }

        public JsonResult GetOrder(int id)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.UpdateCustomer))
            {
                return AccessDeniedJson();
            }

            var order = storeSaleService.GetOrderById(id);
            if (order != null)
            {
                object model = new
                {
                    customer = order.Customer.Name,
                    address = order.Customer.Address,
                    name = order.Name,
                    orderTotal = order.OrderTotal,
                    orderItems = order.OrderItems.Select(orderitem => new
                    {
                        Name = orderitem.Product.Name,
                        PriceOfUnit = orderitem.Price,
                        Quantity = orderitem.Quantity,
                        TotalPrice = orderitem.Price * (decimal)orderitem.Quantity   
                    })
                };
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }

        [HttpGet]
        public JsonResult AutoCompleteCustomers(string name)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.GetCustomerList))
            {
                return AccessDeniedJson();
            }

            var customers = customerService.GetCustomersByName(name);

            var jsons = new List<object>();
            foreach (var customer in customers)
            {
                object o = new
                {
                    customer.Id,
                    customer.Name,
                    customer.Address
                };

                jsons.Add(o);
            }

            return Json(jsons, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AutoCompleteProducts(string name)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.GetStoreProducts))
            {
                return AccessDeniedJson();
            }

            var products = productService.GetAutoCompleteProducts(name);

            var jsons = new List<object>();
            foreach (var product in products)
            {
                object o = new
                {
                    product.Id,
                    product.Name,
                    product.Price
                };

                jsons.Add(o);
            }

            return Json(jsons, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Retail()
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.CreateCustomerOrder))
            {
                return AccessDeniedView();
            }

            return View();
        }

        [HttpPost]
        public JsonResult Create(OrderModel model)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.CreateCustomerOrder))
            {
                return AccessDeniedJson();
            }

            var order = new Order
            {
                CustomerId = model.CustomerId,
                Name = model.Title,
                CreatedOnUtc = DateTime.Now,
                OrderGuid = Guid.NewGuid(),
                StoreId = workContext.CurrentUser.StoreId,
                Payment = new Payment()
            };

            order.Payment.DueDateTime = DateTime.Now.AddDays(30);
            
            decimal totalPrice = 0;
            var orderItems = new List<OrderItem>();
            foreach (var item in model.OrderItems)
            {
                totalPrice += item.PriceOfUnit * (decimal)item.Quantity;
                order.Payment.TotalAmount = (double)totalPrice;
                var orderItem = new OrderItem
                {
                    Quantity = item.Quantity,
                    Price = item.PriceOfUnit,
                    ProductId = item.ProductId,
                    OrderItemGuid = Guid.NewGuid()
                };
                var product = productService.GetProductById(item.ProductId);

                if (product == null)
                {
                    return Json(null);
                }

                orderItem.OriginalProductCost = product.ProductCost;

                orderItems.Add(orderItem);
            }

            order.OrderTotal = totalPrice;
            order.OrderItems = orderItems;

            storeSaleService.AddOrder(order);
            //return RedirectToAction("Index");
            return Json(Url.Action("Index", "StoreSale"));
            //return Json(null);
        }

        public JsonResult OrderList(int skip, int take, int page, int pageSize)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.GetCustomerOrder))
            {
                return AccessDeniedJson();
            }

            var orders = storeSaleService.GetOrders(page, pageSize);
            if (orders != null)
            {
                var orderList = new List<OrderListModel>();

                foreach (var order in orders)
                {
                    var model = new OrderListModel
                    {
                        Id = order.Id,
                        Address = order.Customer.Address,
                        CreatedOn = string.Format("{0:yy-MMM-dd ddd}", order.CreatedOnUtc),
                        Owner = order.Customer.Name,
                        Title = order.Name,
                        TotalPrice = (double)order.OrderTotal
                    };
                    orderList.Add(model);
                }

                return Json(
                    new
                    {
                        total = orders.TotalRecords,
                        data = orderList
                    },
                    JsonRequestBehavior.AllowGet);
            }
            return Json(new {}, JsonRequestBehavior.AllowGet);
        }

       [HttpPost]
        public JsonResult Delete(List<int> ids)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.DeleteCustomerOrder))
            {
                return AccessDeniedJson();
            }

            if (ids != null)
            {
                this.storeSaleService.DeleteOrderByIds(ids);
            }
            return Json(ids);
        }

        [HttpPost]
        public ActionResult UploadRetail()
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.UpdateCustomerOrder))
            {
                return AccessDeniedView();
            }

            HttpPostedFileBase file = Request.Files["retialfile"];
            if (file != null)
            {
                Stream fileStream = file.InputStream;
                XDocument doc = XDocument.Load(fileStream);
                var nodes = doc.Descendants("sale_item");
                List<PostRetail> retails = new List<PostRetail>();
                foreach (var node in nodes)
                {
                    PostRetail retail = new PostRetail();
                    string itemNo = node.Elements("item_number").Select(t => (string) t).FirstOrDefault();

                    var product = this.productService.GetProductByItemNo(itemNo);

                    string date = node.Elements("date").Select(t => (string) t).FirstOrDefault();
                    retail.ProductId = product.Id;
                    retail.StoreId = 0;// this need to update according the user
                    retail.Date = DateTime.ParseExact(date, "yyyyMMdd", new CultureInfo("zh-CN", true));
                    retail.Quantity = node.Elements("sale_quantity").Select(t => (double)t).FirstOrDefault();
                    retail.Price = node.Elements("sale_price").Select(t => (decimal)t).FirstOrDefault();
                    retail.Cost = node.Elements("cost_price").Select(t => (decimal)t).FirstOrDefault();
                    retails.Add(retail);
                }
                this.retailService.AddRecords(retails);

                return RedirectToAction("Retail");
            }

            return Json(null);
        }

        [HttpGet]
        public JsonResult RetailList(int skip, int take, int page, int pageSize)
        {
            if (!this.permissionService.Authorize(StandardPermissionProvider.GetCustomerOrder))
            {
                return AccessDeniedJson();
            }

            var retials = this.retailService.ShowPostRecord(page, pageSize);
            if (retials != null)
            {
                List<object> listModel = new List<object>();
                foreach (var retial in retials)
                {
                    object o = new
                    {
                        ProductName = retial.Product.Name,
                        Price = retial.Price,
                        //Cost = retial.Cost,
                        Quantity = retial.Quantity,
                        Date = retial.Date.ToString(CultureInfo.InvariantCulture),
                        TotalAmount = (decimal) retial.Quantity*retial.Price
                    };

                    listModel.Add(o);
                }
                return Json(
                         new
                         {
                             total = retials.TotalRecords,
                             data = listModel
                         },
                         JsonRequestBehavior.AllowGet);
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }
}