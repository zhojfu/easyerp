namespace EasyERP.Web.Models.Orders
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Infrastructure.Domain.Model;

    public class OrderListModel : BaseEntity
    {
        public OrderListModel()
        {
            AvailableOrderStatuses = new List<SelectListItem>();
            AvailablePaymentStatuses = new List<SelectListItem>();
            AvailableShippingStatuses = new List<SelectListItem>();
            AvailableStores = new List<SelectListItem>();
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