using System.Web.Mvc;

namespace EasyERP.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using Doamin.Service.Products;
    using Doamin.Service.StoreSale;
    using Domain.Model.Orders;
    using EasyERP.Web.Models.StoreSale;
    using Infrastructure.Utility;

    public class StoreSaleController : Controller
    {
        private readonly IStoreSaleService storeSaleService;

        private readonly IProductService productService;

        public StoreSaleController(IStoreSaleService storeSaleService, IProductService productService)
        {
            this.storeSaleService = storeSaleService;
            this.productService = productService;
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