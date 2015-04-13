namespace EasyERP.Web.Models.Orders
{
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class OrderListModel : BaseEntity
    {
        public OrderListModel()
        {
            this.AvailableOrderStatuses = new List<SelectListItem>();
            this.AvailablePaymentStatuses = new List<SelectListItem>();
            this.AvailableShippingStatuses = new List<SelectListItem>();
            this.AvailableStores = new List<SelectListItem>();
        }

        public int OrderStatusId { get; set; }

        public int PaymentStatusId { get; set; }

        public int ShippingStatusId { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        [AllowHtml]
        public string OrderGuid { get; set; }

        public IList<SelectListItem> AvailableOrderStatuses { get; set; }

        public IList<SelectListItem> AvailablePaymentStatuses { get; set; }

        public IList<SelectListItem> AvailableShippingStatuses { get; set; }

        public IList<SelectListItem> AvailableStores { get; set; }
    }
}