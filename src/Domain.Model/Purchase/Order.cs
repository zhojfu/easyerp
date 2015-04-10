namespace Domain.Model.Purchase
{
    using Domain.Model.Base;
    using Domain.Model.Products.Price;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;

    public class Order : BaseEntity
    {
        public string Origin { get; set; }

        public string PartnerRef { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime ApproveDate { get; set; }

        public long SupplierId { get; set; }

        public long DestAddressId { get; set; }

        public long Locationid { get; set; }

        public long PriceListId { get; set; }

        public virtual ProductPriceList PriceList { get; set; }

        public Status State { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; }

        public long ValidatorId { get; set; }

        public virtual User Validator { get; set; }

        public string Notes { get; set; }
    }

    public enum Status
    {
        Draft,

        Sent,

        Bid,

        Confirmed,

        Approved,

        ExceptPicking,

        ExceptInvoide,

        Done,

        Cancel
    }
}