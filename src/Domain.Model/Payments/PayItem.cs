namespace Domain.Model.Payments
{
    using Domain.Model.Orders;
    using Domain.Model.Products;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;

    public class PayItem : BaseEntity, IAggregateRoot
    {
        public DateTime PayDataTime { get; set; }

        public double Paid { get; set; }

        public int PaymentId { get; set; }

        public virtual Payment Payment { get; set; }
    }
}