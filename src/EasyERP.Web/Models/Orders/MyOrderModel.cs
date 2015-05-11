namespace EasyERP.Web.Models.Orders
{
    using EasyERP.Web.Framework.Mvc;
    using EasyERP.Web.Validators.Products;
    using FluentValidation.Attributes;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using EasyERP.Web.Validators.Orders;

    [Validator(typeof(OrderValidator))]
    public class MyOrderModel : BaseModel
    {
        public MyOrderModel()
        {
            AvailableStatuList = new List<SelectListItem>();
            OrderTimeFilterList = new List<SelectListItem>();
        }

        public List<SelectListItem> AvailableStatuList { get; set; }
        public List<SelectListItem> OrderTimeFilterList { get; set; }
    }
}