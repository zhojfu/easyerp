namespace EasyERP.Web.Models.Products
{
    using Domain.Model.Payment;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class InventoryModel
    {
        public InventoryModel()
        {
            AvailableCategories = new List<SelectListItem>();
            AvailableProducts = new List<SelectListItem>();
        }

        public IList<SelectListItem> AvailableCategories { get; set; }

        public IList<SelectListItem> AvailableProducts { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public DateTime DueDateTime { get; set; }

        public float Payables { get; set; }

        public float Paid { get; set; }

        public class PaymentModel
        {
            public DateTime DueDateTime { get; set; }

            public float Payables { get; set; }

            public float Paid { get; set; }
        }
    }
}