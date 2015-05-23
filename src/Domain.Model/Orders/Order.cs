namespace Domain.Model.Orders
{
    using System;
    using System.Collections.Generic;
    using Domain.Model.Customer;
    using Domain.Model.Payments;
    using Domain.Model.Stores;
    using Infrastructure.Domain.Model;

    public class Order : BaseEntity, IAggregateRoot
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        #region Properties

        public Guid OrderGuid { get; set; }

        public int? CustomerId { get; set; }

        public int? StoreId { get; set; }

        public int OrderStatusId { get; set; }

        public int PaymentStatusId { get; set; }

        public decimal OrderTotal { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime ApproveTime { get; set; }

        #endregion Properties

        #region Navigation properties

        public virtual Store Store { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public int PaymentId { get; set; }

        public virtual Payment Payment { get; set; }

        #endregion Navigation properties

        #region Custom properties

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
        Pending = 1,

        Approved = 2,

        Shipped = 3,

        Complete = 4,

        Cancelled = 5
    }

    public enum PaymentStatus
    {
        Pending = 1,

        Paid = 2,

        PartiallyPaid = 3
    }
}