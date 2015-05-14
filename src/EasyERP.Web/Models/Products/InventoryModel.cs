namespace EasyERP.Web.Models.Products
{
    using Domain.Model.Payments;
    using EasyERP.Web.Framework.Mvc;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web.Mvc;

    public class InventoryModel : BaseEntityModel
    {
        public InventoryModel()
        {
            AvailableCategories = new List<SelectListItem>();
            AvailableProducts = new List<SelectListItem>();
        }

        [DisplayName("产品目录")]
        public int SelectedCategoryId { get; set; }

        public IList<SelectListItem> AvailableCategories { get; set; }

        public IList<SelectListItem> AvailableProducts { get; set; }

        [DisplayName("产品名")]
        public int ProductId { get; set; }

        [DisplayName("产品数量:")]
        public int Quantity { get; set; }

        public ICollection<Payment> Payments { get; set; }

        [DisplayName("付款截止日期：")]
        public DateTime DueDateTime { get; set; }

        [DisplayName("应付款：")]
        public float Payables { get; set; }

        [DisplayName("已付款：")]
        public float Paid { get; set; }

        public class PaymentModel : BaseEntityModel
        {
            public DateTime DueDateTime { get; set; }

            public float Payables { get; set; }

            public float Paid { get; set; }
        }
    }
}