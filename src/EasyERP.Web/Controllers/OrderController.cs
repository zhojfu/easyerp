using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyERP.Web.Controllers
{
    using Doamin.Service.Order;
    using Doamin.Service.Products;
    using Doamin.Service.Security;
    using Doamin.Service.Stores;
    using Domain.Model.Orders;
    using EasyERP.Web.Framework;
    using EasyERP.Web.Kendoui;
    using EasyERP.Web.Models.Orders;

    public class OrderController : BaseAdminController
    {
        private readonly IPermissionService _permissionService;

        private readonly IStoreService _storeService;

        private readonly IProductService _productService;

        private readonly IOrderService _orderService;

        public OrderController(IPermissionService permissionService, IStoreService storeService,

            IProductService productService, IOrderService orderService

            )
        {
            this._permissionService = permissionService;
            this._storeService = storeService;
            this._productService = productService;
            this._orderService = orderService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List(int? orderStatusId = null,
            int? paymentStatusId = null, int? shippingStatusId = null)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return AccessDeniedView();

            //order statuses
            var model = new OrderListModel();
            model.AvailableOrderStatuses = OrderStatus.Pending.ToSelectList(false).ToList();
            model.AvailableOrderStatuses.Insert(0, new SelectListItem { Text = "All", Value = "0" });
            if (orderStatusId.HasValue)
            {
                //pre-select value?
                var item = model.AvailableOrderStatuses.FirstOrDefault(x => x.Value == orderStatusId.Value.ToString());
                if (item != null)
                    item.Selected = true;
            }

            //payment statuses
            model.AvailablePaymentStatuses = PaymentStatus.Pending.ToSelectList(false).ToList();
            model.AvailablePaymentStatuses.Insert(0, new SelectListItem { Text = "All", Value = "0" });
            if (paymentStatusId.HasValue)
            {
                //pre-select value?
                var item = model.AvailablePaymentStatuses.FirstOrDefault(x => x.Value == paymentStatusId.Value.ToString());
                if (item != null)
                    item.Selected = true;
            }

            //shipping statuses
            model.AvailableShippingStatuses = ShippingStatus.NotYetShipped.ToSelectList(false).ToList();
            model.AvailableShippingStatuses.Insert(0, new SelectListItem { Text = "All", Value = "0" });
            if (shippingStatusId.HasValue)
            {
                //pre-select value?
                var item = model.AvailableShippingStatuses.FirstOrDefault(x => x.Value == shippingStatusId.Value.ToString());
                if (item != null)
                    item.Selected = true;
            }

            //stores
            model.AvailableStores.Add(new SelectListItem { Text = "All", Value = "0" });
            foreach (var s in _storeService.GetAllStores())
                model.AvailableStores.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString() });
            model.AvailableStores.Insert(0, new SelectListItem() { Text = "All", Value = "0" });

            return View(model);
        }

        [HttpPost]
        public ActionResult OrderList(DataSourceRequest command, OrderListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return AccessDeniedView();

            OrderStatus? orderStatus = model.OrderStatusId > 0 ? (OrderStatus?)(model.OrderStatusId) : null;
            PaymentStatus? paymentStatus = model.PaymentStatusId > 0 ? (PaymentStatus?)(model.PaymentStatusId) : null;
            ShippingStatus? shippingStatus = model.ShippingStatusId > 0 ? (ShippingStatus?)(model.ShippingStatusId) : null;

            var filterByProductId = 0;
            var product = _productService.GetProductById(model.ProductId);
            if (product != null)
                filterByProductId = model.ProductId;

            //load orders
            var orders = _orderService.SearchOrders(storeId: model.CustomerId,
                productId: filterByProductId,
                os: orderStatus,
                ps: paymentStatus,
                ss: shippingStatus,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = orders.Select(x =>
                {
                    var store = _storeService.GetStoreById(x.CustomerId);
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

            ////summary report
            //var reportSummary = _orderReportService.GetOrderAverageReportLine(model.StoreId,
            //    model.VendorId, 0,
            //    orderStatus, paymentStatus, shippingStatus,
            //    startDateValue, endDateValue, model.CustomerEmail);
            //var profit = _orderReportService.ProfitReport(model.StoreId,
            //    model.VendorId, 0,
            //    orderStatus, paymentStatus, shippingStatus,
            //    startDateValue, endDateValue, model.CustomerEmail);
            //var primaryStoreCurrency = _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId);
            //if (primaryStoreCurrency == null)
            //    throw new Exception("Cannot load primary store currency");

            //gridModel.ExtraData = new OrderAggreratorModel
            //{
            //    aggregatorprofit = _priceFormatter.FormatPrice(profit, true, false),
            //    aggregatorshipping = _priceFormatter.FormatShippingPrice(reportSummary.SumShippingExclTax, true, primaryStoreCurrency, _workContext.WorkingLanguage, false),
            //    aggregatortax = _priceFormatter.FormatPrice(reportSummary.SumTax, true, false),
            //    aggregatortotal = _priceFormatter.FormatPrice(reportSummary.SumOrders, true, false)
            //};
            return new JsonResult
            {
                Data = gridModel
            };
        }

        //[HttpPost, ActionName("List")]
        //[FormValueRequired("go-to-order-by-number")]
        //public ActionResult GoToOrderId(OrderListModel model)
        //{
        //    var order = _orderService.GetOrderById(model.GoDirectlyToNumber);
        //    if (order == null)
        //        return List();

        //    return RedirectToAction("Edit", "Order", new { id = order.Id });
        //}

        public ActionResult ProductSearchAutoComplete(string term)
        {
            const int searchTermMinimumLength = 3;
            if (String.IsNullOrWhiteSpace(term) || term.Length < searchTermMinimumLength)
                return Content("");

            //products
            const int productNumber = 15;
            var products = _productService.SearchProducts(
                keywords: term,
                pageSize: productNumber,
                showHidden: true);

            var result = (from p in products
                          select new
                          {
                              label = p.Name,
                              productid = p.Id
                          })
                          .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}