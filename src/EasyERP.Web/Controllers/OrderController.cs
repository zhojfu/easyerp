namespace EasyERP.Web.Controllers
{
    using Doamin.Service.Order;
    using Doamin.Service.Products;
    using Doamin.Service.Security;
    using Doamin.Service.Stores;
    using Domain.Model.Orders;
    using EasyERP.Web.Framework;
    using EasyERP.Web.Framework.Kendoui;
    using EasyERP.Web.Models.Orders;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public class OrderController : BaseAdminController
    {
        private readonly IOrderService orderService;

        private readonly IPermissionService permissionService;

        private readonly IProductService productService;

        private readonly IStoreService storeService;

        public OrderController(
            IPermissionService permissionService,
            IStoreService storeService,
            IProductService productService,
            IOrderService orderService
            )
        {
            this.permissionService = permissionService;
            this.storeService = storeService;
            this.productService = productService;
            this.orderService = orderService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List(
            int? orderStatusId = null,
            int? paymentStatusId = null,
            int? shippingStatusId = null)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.ManageOrders))
            {
                return AccessDeniedView();
            }

            //order statuses
            var model = new OrderListModel();
            model.AvailableOrderStatuses = OrderStatus.Pending.ToSelectList(false).ToList();
            model.AvailableOrderStatuses.Insert(
                0,
                new SelectListItem
                {
                    Text = "All",
                    Value = "0"
                });
            if (orderStatusId.HasValue)
            {
                //pre-select value?
                var item = model.AvailableOrderStatuses.FirstOrDefault(x => x.Value == orderStatusId.Value.ToString());
                if (item != null)
                {
                    item.Selected = true;
                }
            }

            //payment statuses
            model.AvailablePaymentStatuses = PaymentStatus.Pending.ToSelectList(false).ToList();
            model.AvailablePaymentStatuses.Insert(
                0,
                new SelectListItem
                {
                    Text = "All",
                    Value = "0"
                });
            if (paymentStatusId.HasValue)
            {
                //pre-select value?
                var item =
                    model.AvailablePaymentStatuses.FirstOrDefault(x => x.Value == paymentStatusId.Value.ToString());
                if (item != null)
                {
                    item.Selected = true;
                }
            }

            //shipping statuses
            model.AvailableShippingStatuses = ShippingStatus.NotYetShipped.ToSelectList(false).ToList();
            model.AvailableShippingStatuses.Insert(
                0,
                new SelectListItem
                {
                    Text = "All",
                    Value = "0"
                });
            if (shippingStatusId.HasValue)
            {
                //pre-select value?
                var item =
                    model.AvailableShippingStatuses.FirstOrDefault(x => x.Value == shippingStatusId.Value.ToString());
                if (item != null)
                {
                    item.Selected = true;
                }
            }

            //stores
            model.AvailableStores.Add(
                new SelectListItem
                {
                    Text = "All",
                    Value = "0"
                });
            foreach (var s in storeService.GetAllStores())
            {
                model.AvailableStores.Add(
                    new SelectListItem
                    {
                        Text = s.Name,
                        Value = s.Id.ToString()
                    });
            }
            model.AvailableStores.Insert(
                0,
                new SelectListItem
                {
                    Text = "All",
                    Value = "0"
                });

            return View(model);
        }

        [HttpPost]
        public ActionResult OrderList(DataSourceRequest command, OrderListModel model)
        {
            if (!permissionService.Authorize(StandardPermissionProvider.ManageOrders))
            {
                return AccessDeniedView();
            }

            var orderStatus = model.OrderStatusId > 0 ? (OrderStatus?)(model.OrderStatusId) : null;
            var paymentStatus = model.PaymentStatusId > 0 ? (PaymentStatus?)(model.PaymentStatusId) : null;
            var shippingStatus = model.ShippingStatusId > 0 ? (ShippingStatus?)(model.ShippingStatusId) : null;

            var filterByProductId = 0;
            var product = productService.GetProductById(model.ProductId);
            if (product != null)
            {
                filterByProductId = model.ProductId;
            }

            //load orders
            var orders = orderService.SearchOrders(
                model.CustomerId,
                filterByProductId,
                orderStatus,
                paymentStatus,
                shippingStatus,
                command.Page - 1,
                command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = orders.Select(
                    x =>
                    {
                        var store = storeService.GetStoreById(x.CustomerId);
                        return new OrderModel
                        {
                            Id = x.Id,
                            StoreName = store != null ? store.Name : "Unknown",
                            OrderTotal = x.OrderTotal.ToString(),
                            OrderStatus = x.OrderStatus.ToString(),
                            PaymentStatus = x.PaymentStatus.ToString(),
                            ShippingStatus = x.ShippingStatus.ToString(),
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

        public ActionResult ProductSearchAutoComplete(string term)
        {
            const int SearchTermMinimumLength = 3;
            if (String.IsNullOrWhiteSpace(term) ||
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
            var model = new OrderModel();
            return View(model);
        }
    }
}