namespace EasyERP.Web.Models.Orders
{
    using EasyERP.Web.Framework.Mvc;
    using EasyERP.Web.Validators.Products;
    using FluentValidation.Attributes;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    [Validator(typeof(ProductValidator))]
    public class OrderItemModel : BaseEntityModel
    {
        public Guid OrderItemGuid { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal OriginalProductCost { get; set; }
        public string ProductName { get; set; }
    }
}