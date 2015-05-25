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
    using EasyERP.Web.Models.StoreSale;

    public class StoreSaleController : Controller
    {
        private readonly ICustomerService customerService;

        private readonly IProductService productService;

        private readonly IStoreSaleService storeSaleService;

        public StoreSaleController(
            IStoreSaleService storeSaleService,
            IProductService productService,
            ICustomerService customerService)
        {
            this.storeSaleService = storeSaleService;
            this.productService = productService;
            this.customerService = customerService;
        }

        // GET: /StoreSales/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddOrderItem(OrderItemModel orderItem)
        {
            return Json(orderItem);
        }

        [HttpPost]
        public JsonResult DeleteOrderItem(OrderItemModel orderItem)
        {
            return Json(orderItem);
        }

        [HttpPost]
        public JsonResult UpdateOrderItem(OrderItemModel orderItem)
        {
            return Json(orderItem);
        }

        [HttpGet]
        public JsonResult AutoCompleteCustomers(string name)
        {
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
            return View();
        }

        [HttpPost]
        public JsonResult Create(OrderModel model)
        {
            var order = new Order
            {
                CustomerId = model.CustomerId,
                Name = model.Title,
                CreatedOnUtc = DateTime.Now,
                OrderGuid = Guid.NewGuid(),
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

            return Json(null);
        }

        public JsonResult OrderList(int skip, int take, int page, int pageSize)
        {
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
            if (ids != null)
            {
                this.storeSaleService.DeleteOrderByIds(ids);
            }
            return Json(ids);
        }
    }
}