namespace EasyERP.Web.Models.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web.Mvc;
    using EasyERP.Web.Framework.Mvc;
    using EasyERP.Web.Validators.Products;
    using FluentValidation.Attributes;

    [Validator(typeof(ProductValidator))]
    public class ProductModel : BaseEntityModel
    {
        public ProductModel()
        {
            AvailableCategories = new List<SelectListItem>();
        }

        [AllowHtml]
        [DisplayName("产品名称")]
        public string Name { get; set; }

        [DisplayName("产品编号")]
        public string ItemNo { get; set; }

        [AllowHtml]
        [DisplayName("简介")]
        public string ShortDescription { get; set; }

        [AllowHtml]
        [DisplayName("详情描述")]
        public string FullDescription { get; set; }

        [AllowHtml]
        [DisplayName("条码")]
        public string Gtin { get; set; }

        [DisplayName("产地")]
        public string Origin { get; set; }

        [DisplayName("库存量")]
        public int StockQuantity { get; set; }

        [DisplayName("当前价格")]
        public decimal Price { get; set; }

        [DisplayName("成本")]
        public decimal ProductCost { get; set; }

        [DisplayName("重量")]
        public decimal Weight { get; set; }

        [DisplayName("重量")]
        public decimal Length { get; set; }

        [DisplayName("宽度")]
        public decimal Width { get; set; }

        [DisplayName("高度")]
        public decimal Height { get; set; }

        public bool Published { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string CategoryName { get; set; }

        [DisplayName("目录名称:")]
        public string CategoryId { get; set; }

        //categories
        public IList<SelectListItem> AvailableCategories { get; set; }
    }
}