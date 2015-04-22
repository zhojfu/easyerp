namespace Domain.Model.Orders
{
    using System;
    using System.Collections.Generic;
    using Domain.Model.Stores;
    using Infrastructure.Domain.Model;

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

        /// <summary>
        /// Gets or sets the order status
        /// </summary>
        public OrderStatus OrderStatus
        {
            get { return (OrderStatus)OrderStatusId; }
            set { OrderStatusId = (int)value; }
        }

        /// <summary>
        /// Gets or sets the payment status
        /// </summary>
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
        /// <summary>
        /// Shipping not required
        /// </summary>
        ShippingNotRequired = 10,

        /// <summary>
        /// Not yet shipped
        /// </summary>
        NotYetShipped = 20,

        /// <summary>
        /// Partially shipped
        /// </summary>
        PartiallyShipped = 25,

        /// <summary>
        /// Shipped
        /// </summary>
        Shipped = 30,

        /// <summary>
        /// Delivered
        /// </summary>
        Delivered = 40
    }
}