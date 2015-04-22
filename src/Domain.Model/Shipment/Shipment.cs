namespace Domain.Model.Shipment
{
    using Domain.Model.Orders;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;

    public class Shipment : BaseEntity
    {
        private ICollection<ShipmentItem> _shipmentItems;

        public int OrderId { get; set; }

        public string TrackingNumber { get; set; }

        public decimal? TotalWeight { get; set; }

        public DateTime? ShippedDateUtc { get; set; }

        public DateTime? DeliveryDateUtc { get; set; }

        public string AdminComment { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public virtual Order Order { get; set; }

        public virtual ICollection<ShipmentItem> ShipmentItems
        {
            get { return _shipmentItems ?? (_shipmentItems = new List<ShipmentItem>()); }
            protected set { _shipmentItems = value; }
        }
    }
}