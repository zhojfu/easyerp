namespace Domain.Model.Payments
{
    using System;
    using Infrastructure.Domain.Model;

    public class PayItem : BaseEntity, IAggregateRoot
    {
        public DateTime PayDataTime { get; set; }

        public double Paid { get; set; }

        public int PaymentId { get; set; }

        public virtual Payment Payment { get; set; }
    }
}