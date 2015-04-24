namespace Domain.Model.Orders
{
    using Domain.Model.Stores;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;

    public class Order : BaseEntity, IAggregateRoot
    {
        private ICollection<OrderItem> orderItems;

        #region Properties

        public Guid OrderGuid { get; set; }

        public int CustomerId { get; set; }

        public int OrderStatusId { get; set; }

        public int ShippingStatusId { get; set; }

        public int PaymentStatusId { get; set; }

        public decimal OrderSubtotal { get; set; }

        public decimal OrderTotal { get; set; }

        public DateTime? PaidDateUtc { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime ApproveTime { get; set; }

        #endregion Properties

        #region Navigation properties

        public virtual Store Customer { get; set; }

        public virtual ICollection<OrderItem> OrderItems
        {
            get { return orderItems ?? (orderItems = new List<OrderItem>()); }
            protected set { orderItems = value; }
        }

        #endregion Navigation properties

        #region Custom properties

        public ShippingStatus ShippingStatus
        {
            get { return (ShippingStatus)ShippingStatusId; }
            set { ShippingStatusId = (int)value; }
        }

        public OrderStatus OrderStatus
        {
            get { return (OrderStatus)OrderStatusId; }
            set { OrderStatusId = (int)value; }
        }

        public PaymentStatus PaymentStatus
        {
            get { return (PaymentStatus)PaymentStatusId; }
            set { PaymentStatusId = (int)value; }
        }

        #endregion Custom properties
    }

    public enum OrderStatus
    {
        Pending = 10,

        Processing = 20,

        Complete = 30,

        Cancelled = 40
    }

    public enum PaymentStatus
    {
        Pending = 10,

        Paid = 20,

        PartiallyPaid = 30
    }

    public enum ShippingStatus
    {
        ShippingNotRequired = 10,

        NotYetShipped = 20,

        PartiallyShipped = 25,

        Shipped = 30,

        Delivered = 40
    }
}