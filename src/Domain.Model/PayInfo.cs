namespace Domain.Model
{
    using Infrastructure.Domain.Model;
    using System;

    public class PayInfo : BaseEntity, IAggregateRoot
    {
        public Guid OrderId { get; set; }

        public DateTime PayDateTime { get; set; }

        public decimal PayAmount { get; set; }
    }
}