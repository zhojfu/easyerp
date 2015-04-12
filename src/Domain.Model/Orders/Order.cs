namespace Domain.Model.Orders
{
    using Domain.Model.Discounts;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;

    public class Order : BaseEntity, IAggregateRoot
    {
        private ICollection<OrderItem> orderItems;

        #region Properties

        public Guid OrderGuid { get; set; }

        public int StoreId { get; set; }

        public int CustomerId { get; set; }

        public int OrderStatusId { get; set; }

        public int ShippingStatusId { get; set; }

        public int PaymentStatusId { get; set; }

        public decimal OrderSubtotal { get; set; }

        public DateTime? PaidDateUtc { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        #endregion Properties

        #region Navigation properties

        public virtual ICollection<OrderItem> OrderItems
        {
            get { return this.orderItems ?? (this.orderItems = new List<OrderItem>()); }
            protected set { this.orderItems = value; }
        }

        #endregion Navigation properties

        #region Custom properties

        /// <summary>
        /// Gets or sets the order status
        /// </summary>
        public OrderStatus OrderStatus
        {
            get
            {
                return (OrderStatus)this.OrderStatusId;
            }
            set
            {
                this.OrderStatusId = (int)value;
            }
        }

        /// <summary>
        /// Gets or sets the payment status
        /// </summary>
        public PaymentStatus PaymentStatus
        {
            get
            {
                return (PaymentStatus)this.PaymentStatusId;
            }
            set
            {
                this.PaymentStatusId = (int)value;
            }
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
        PartiallyPaid = 30,
    }
}