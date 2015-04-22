namespace Domain.Model.Shipment
{
    using Infrastructure.Domain.Model;

    public class ShipmentItem : BaseEntity
    {
        public int ShipmentId { get; set; }

        public int OrderItemId { get; set; }

        public int Quantity { get; set; }

        public int WarehouseId { get; set; }

        public virtual Shipment Shipment { get; set; }
    }
}