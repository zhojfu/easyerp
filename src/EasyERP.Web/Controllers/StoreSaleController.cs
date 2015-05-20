using System.Web.Mvc;

namespace EasyERP.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using Doamin.Service.Customer;
    using Doamin.Service.Products;
    using Doamin.Service.StoreSale;
    using Domain.Model.Orders;
    using EasyERP.Web.Models.StoreSale;
    using Infrastructure.Utility;

    public class StoreSaleController : Controller
    {
        private readonly IStoreSaleService storeSaleService;

        private readonly IProductService productService;

        private readonly ICustomerService customerService;

        public StoreSaleController(IStoreSaleService storeSaleService, IProductService productService, ICustomerService customerService)
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
            var customers = this.customerService.GetCustomersByName(name);
            
            List<object> jsons = new List<object>();
            foreach (var customer in customers)
            {
                object o = new
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Address = customer.Address
                };

                jsons.Add(o);
            }

            return Json(jsons, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AutoCompleteProducts(string name)
        {
            var products = this.productService.GetAutoCompleteProducts(name);

            List<object> jsons = new List<object> { 
                new {Id = 1, Name="米", Price = 10},
                new {Id = 1, Name="盐", Price = 15},
                new {Id = 1, Name="油1", Price = 11},
                new {Id = 1, Name="油2", Price = 12},
                new {Id = 1, Name="油3", Price = 13},
                new {Id = 1, Name="油", Price = 20},
                new {Id = 1, Name="柴", Price = 30},
                new {Id = 1, Name="酱", Price = 90},
                new {Id = 1, Name="醋", Price = 50},
                new {Id = 1, Name="茶", Price = 10}
            };

            foreach (var product in products)
            {
                object o = new
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                };

                jsons.Add(o);
            }

            return Json(jsons, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(OrderModel model)
        {
            Order order = new Order
            {
                CustomerId = model.CustomerId,
                Name = model.Title,
                CreatedOnUtc = DateTime.Now
            };

            decimal totalPrice = 0;
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (var item in model.OrderItems)
            {
                totalPrice += item.PriceOfUnit * (decimal)item.Quantity;
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    Quantity = item.Quantity,
                    Price = item.PriceOfUnit,
                    ProductId = item.ProductId,
                    OrderItemGuid = Guid.NewGuid()
                };
                var product = productService.GetProductById(item.ProductId);
                orderItem.OriginalProductCost = product.ProductCost;
                
                orderItems.Add(orderItem);
            }

            order.OrderTotal = totalPrice;
            order.OrderItems = orderItems;
            
            this.storeSaleService.AddOrder(order);

            return Json(null);
        }

        public JsonResult OrderList(int skip, int take, int page, int pageSize)
        {
            PagedResult<Order> orders = this.storeSaleService.GetOrders(page, pageSize);
            if (orders != null)
            {
                List<OrderListModel> orderList = new List<OrderListModel>();

                foreach (var order in orders)
                {
                    OrderListModel model = new OrderListModel
                    {
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
            return Json(null);
        }

	}
}