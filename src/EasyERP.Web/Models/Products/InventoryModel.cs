
namespace EasyERP.Web.Models.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web.Mvc;
    using EasyERP.Web.Framework.Mvc;
    using EasyERP.Web.Validators.Products;
    using FluentValidation.Attributes;

    [Validator(typeof(InventoryValidator))]
    public class InventoryModel : BaseEntityModel
    {
        public InventoryModel()
        {
            AvailableCategories = new List<SelectListItem>();
            AvailableProducts = new List<SelectListItem>();
            AvailableStores = new List<SelectListItem>();
        }

        [DisplayName("产品目录:")]
        public int SelectedCategoryId { get; set; }

        public IList<SelectListItem> AvailableCategories { get; set; }

        public IList<SelectListItem> AvailableProducts { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; }

        [DisplayName("产品名:")]
        public int ProductId { get; set; }

        [DisplayName("门店名:")]
        public int StoreId { get; set; }

        [DisplayName("产品数量:")]
        public float Quantity { get; set; }

        [DisplayName("付款截止日期：")]
        public DateTime DueDateTime { get; set; }

        [DisplayName("应付款：")]
        public float TotalAmount { get; set; }

        [DisplayName("已付款：")]
        public float Paid { get; set; }

        [DisplayName("说明：")]
        public string Note { get; set; }
    }
}