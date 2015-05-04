namespace EasyERP.Web.Models.Orders
{
    using EasyERP.Web.Framework.Mvc;
    using EasyERP.Web.Validators.Products;
    using FluentValidation.Attributes;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using EasyERP.Web.Validators.Orders;

    [Validator(typeof(OrderValidator))]
    public class OrderModel : BaseEntityModel
    {
        public OrderModel()
        {
            Items = new List<OrderItemModel>();
        }

        public Guid OrderGuid { get; set; }

        public string StoreName { get; set; }

        public string OrderSubtotal { get; set; }

        public string OrderTotal { get; set; }

        public string Profit { get; set; }

        public string OrderStatus { get; set; }

        public string ShippingStatus { get; set; }

        public int OrderStatusId { get; set; }

        public string PaymentStatus { get; set; }

        public IList<OrderItemModel> Items { get; set; }

        public DateTime CreatedOn { get; set; }

        //workflow info
        public bool CanCancelOrder { get; set; }

        public bool CanCapture { get; set; }

        public bool CanMarkOrderAsPaid { get; set; }

        public bool CanRefund { get; set; }

        public bool CanRefundOffline { get; set; }

        public bool CanPartiallyRefund { get; set; }

        public bool CanPartiallyRefundOffline { get; set; }

        public bool CanVoid { get; set; }

        public bool CanVoidOffline { get; set; }

        public class OrderItemModel : BaseEntity
        {
            public int ProductId { get; set; }

            public string ProductName { get; set; }

            public string Sku { get; set; }

            public int Quantity { get; set; }

            public string SubTotal { get; set; }
        }

        public class AddOrderProductModel : BaseEntityModel
        {
            public AddOrderProductModel()
            {
                AvailableCategories = new List<SelectListItem>();
                AvailableManufacturers = new List<SelectListItem>();
                AvailableProductTypes = new List<SelectListItem>();
            }

            [AllowHtml]
            public string SearchProductName { get; set; }

            public int SearchCategoryId { get; set; }

            public int SearchManufacturerId { get; set; }

            public int SearchProductTypeId { get; set; }

            public IList<SelectListItem> AvailableCategories { get; set; }

            public IList<SelectListItem> AvailableManufacturers { get; set; }

            public IList<SelectListItem> AvailableProductTypes { get; set; }

            public int OrderId { get; set; }

            public class ProductModel : BaseEntity
            {
                [AllowHtml]
                public string Name { get; set; }

                [AllowHtml]
                public string Sku { get; set; }
            }
        }
    }
}