namespace EasyERP.Web.Models.Orders
{
    using EasyERP.Web.Framework.Mvc;
    using EasyERP.Web.Validators.Orders;
    using EasyERP.Web.Validators.Products;
    using FluentValidation.Attributes;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

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

        public decimal OrderTotal { get; set; }

        public string Profit { get; set; }

        public int OrderStatus { get; set; }

        public int OrderStatusId { get; set; }

        public string PaymentStatus { get; set; }

        public IList<OrderItemModel> Items { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}