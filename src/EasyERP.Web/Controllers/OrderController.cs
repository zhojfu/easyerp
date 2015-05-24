namespace EasyERP.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Doamin.Service.Order;
    using Doamin.Service.Products;
    using Doamin.Service.Security;
    using Doamin.Service.Stores;
    using Domain.Model.Orders;
    using Domain.Model.Payments;
    using EasyErp.Core;
    using EasyERP.Web.Framework.Kendoui;
    using EasyERP.Web.Models.Orders;

    public class OrderController : BaseAdminController
    {
        private readonly IOrderService orderService;

        private readonly IPermissionService permissionService;

        private readonly IProductService productService;

        private readonly IStoreService storeService;

        private readonly IWorkContext workContext;

        public OrderController(
            IPermissionService permissionService,
            IStoreService storeService,
            IProductService productService,
            IOrderService orderService,
            IWorkContext workContext
            )
        {
            this.permissionService = permissionService;
            this.storeService = storeService;
            this.productService = productService;
            this.orderService = orderService;
            this.workContext = workContext;
        }

        public ActionResult Index()
        {
            return RedirectToAction("MyOrder");
        }

        public ActionResult ProductSearchAutoComplete(string term)
        {
            const int SearchTermMinimumLength = 3;
            if (string.IsNullOrWhiteSpace(term) ||
                term.Length < SearchTermMinimumLength)
            {
                return Content("");
            }

            //products
            const int ProductNumber = 15;
            var products = productService.SearchProducts(
                keywords: term,
                pageSize: ProductNumber);

            var result = (from p in products
                          select new
                          {
                              label = p.Name,
                              productid = p.Id
                          })
                .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProductList(DataSourceRequest data, OrderModel model)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return AccessDeniedView();
            }

            var products = productService.SearchProducts();
            var gridModel = new DataSourceResult
            {
                Data = products.Select(
                    x => new
                    {
                        id = x.Id,
                        name = x.Name,
                        price = x.Price
                    }),
                Total = products.TotalCount
            };

            return Json(gridModel);
        }

        public ActionResult Detail(Guid guid)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return AccessDeniedView();
            }

            var order = orderService.GetOrderByGuid(guid);
            return View();
        }

        [HttpPost]
        public ActionResult OrderUpdate(DataSourceRequest request, IEnumerable<CartItemModel> cartItems)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return AccessDeniedView();
            }

            if (cartItems == null)
            {
                return RedirectToAction("Create", "Order");
            }

            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    OrderGuid = Guid.NewGuid(),
                    OrderStatus = OrderStatus.Pending,
                    CreatedOnUtc = DateTime.Now,
                    StoreId = workContext.CurrentUser.StoreId,
                    PaymentStatus = PaymentStatus.Pending
                };
                order.OrderItems = cartItems.Select(
                    c => new OrderItem
                    {
                        Order = order,
                        OriginalProductCost = productService.GetProductById(c.ProductId).ProductCost,
                        OrderItemGuid = Guid.NewGuid(),
                        Price = c.Price,
                        ProductId = c.ProductId,
                        Quantity = c.Quantity
                    }).ToList();

                order.OrderTotal = order.OrderItems.Sum(o => o.Price * (decimal)o.Quantity);
                order.Payment = new Payment
                {
                    DueDateTime = DateTime.Now.AddDays(45),
                    Order = order,
                    TotalAmount = (double)order.OrderTotal
                };

                orderService.InsertOrder(order);
                return Json(Url.Action("MyOrder", "Order"));
            }

            return Json(Url.Action("Create", "Order"));
        }

        public ActionResult MyOrder()
        {
            return View();
        }

        [HttpPost]
        public JsonResult MyOrder(DataSourceRequest request, int orderStatus)
        {
            var status = orderStatus > 0 ? (OrderStatus?)orderStatus : null;
            var storeId = workContext.CurrentUser.StoreId;
            var orders = orderService.SearchOrders(storeId, 0, 0, status).ToList();
            var dataSourceResult = new DataSourceResult
            {
                Data = orders.Select(
                    o => new
                    {
                        orderGuid = o.OrderGuid,
                        createdTime = o.CreatedOnUtc,
                        totalPrice = o.OrderTotal,
                        orderStatus = o.OrderStatus,
                        orderItems = o.OrderItems.Select(
                            i => new
                            {
                                productName = i.Product.Name,
                                quantity = i.Quantity,
                                itemPrice = i.Price,
                                total = (decimal)i.Quantity * i.Price
                            })
                    }),
                Total = orders.Count
            };
            return Json(dataSourceResult);
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OrderList(DataSourceRequest command, SearchModel model)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.ManageOrders))
            {
                return AccessDeniedView();
            }

            var orderStatus = model.OrderStatus > 0 ? (OrderStatus?)(model.OrderStatus) : null;
            var payStatus = model.PayStatus > 0 ? (PaymentStatus?)(model.OrderStatus) : null;

            //load orders
            var orders = orderService.SearchOrders(
                model.StoreId,
                0,
                0,
                orderStatus,
                payStatus,
                command.Page - 1,
                command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = orders.Select(
                    x =>
                    {
                        var store = storeService.GetStoreById(x.StoreId.GetValueOrDefault());
                        return new OrderModel
                        {
                            Id = x.Id,
                            OrderGuid = x.OrderGuid,
                            StoreName = store != null ? store.Name : "Unknown",
                            OrderTotal = x.OrderTotal,
                            OrderStatus = (int)x.OrderStatus,
                            PaymentStatus = x.PaymentStatus.ToString(),
                            CreatedOn = x.CreatedOnUtc
                        };
                    }),
                Total = orders.TotalCount
            };

            return new JsonResult
            {
                Data = gridModel
            };
        }

        public ActionResult Review(Guid orderGuid)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return AccessDeniedView();
            }

            var order = orderService.GetOrderByGuid(orderGuid);
            if (order == null)
            {
                return RedirectToAction("MyOrder");
            }
            var orderModel = new OrderModel
            {
                Id = order.Id,
                OrderGuid = order.OrderGuid,
                StoreName = order.Store.Name,
                OrderTotal = order.OrderTotal,
                OrderStatus = (int)order.OrderStatus,
                PaymentStatus = order.PaymentStatus.ToString(),
                CreatedOn = order.CreatedOnUtc,
                Items = order.OrderItems.Select(
                    i => new OrderItemModel
                    {
                        Price = i.Price,
                        Quantity = i.Quantity,
                        ProductName = i.Product.Name
                    }).ToList()
            };

            return View(orderModel);
        }

        [HttpPost]
        public ActionResult Review(OrderModel orderModel)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return AccessDeniedView();
            }

            return null;
        }


        [HttpPost]
        public ActionResult ChangeStatus(Guid orderGuid, int status)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            {
                return AccessDeniedView();
            }

            if (status < 1 && status > 5)
            {
                return HttpNotFound();
            }
            
            var order = orderService.GetOrderByGuid(orderGuid);
            if (order == null)
            {
                return RedirectToAction("MyOrder");
            }

            order.OrderStatus = (OrderStatus)status;
            order.ApproveTime = DateTime.Now;
            orderService.UpdateOrder(order);

            return Json(order.OrderStatus);
        }
    }
}