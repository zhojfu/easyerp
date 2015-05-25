namespace EasyERP.Web.Models.Orders
{
    using EasyERP.Web.Framework.Mvc;
    using EasyERP.Web.Validators.Orders;
    using FluentValidation.Attributes;
    using System;
    using System.Collections.Generic;

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

        public string OrderStatus { get; set; }

        public int OrderStatusId { get; set; }

        public int PaymentStatus { get; set; }

        public double PaidAmount { get; set; }

        public int PaymentId { get; set; }

        public IList<OrderItemModel> Items { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}