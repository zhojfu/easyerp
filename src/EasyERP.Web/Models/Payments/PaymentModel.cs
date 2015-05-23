namespace EasyERP.Web.Models.Payments
{
    using EasyERP.Web.Framework.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

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