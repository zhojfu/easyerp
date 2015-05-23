namespace EasyERP.Web.Models.Payments
{
    using System;
    using EasyERP.Web.Framework.Mvc;

    public class PaymentModel : BaseEntityModel
    {
        public double TotalAmount { get; set; }

        public double Paid { get; set; }

        public DateTime DueDateTime { get; set; }
    }

    public class PayItemModel
    {
        public int Id { get; set; }

        //public DateTime PayDate { get; set; }

        public double PayAmount { get; set; }
    }
}