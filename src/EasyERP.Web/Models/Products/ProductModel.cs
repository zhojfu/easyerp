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
        [DisplayName("产品简介")]
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

        [DisplayName("售价")]
        public decimal Price { get; set; }

        [DisplayName("进货价")]
        public decimal ProductCost { get; set; }

        [DisplayName("重量")]
        public decimal Weight { get; set; }

        [DisplayName("长度")]
        public decimal Length { get; set; }

        [DisplayName("宽度")]
        public decimal Width { get; set; }

        [DisplayName("高度")]
        public decimal Height { get; set; }


        [DisplayName("规格")]
        public string Specification { get; set; }

        [DisplayName("生产厂名")]
        public string VenderName { get; set; }
        [DisplayName("厂址")]
        public string VenderAddres { get; set; }
        [DisplayName("保质期")]
        public string ShelfLifeDays { get; set; }
        [DisplayName("主要成份及含量")]
        public string Composition { get; set; }
        [DisplayName("生产批号")]
        public string ProductionBatchNumber { get; set; }
        [DisplayName("产品标准号")]
        public string ProductStandardsNumber { get; set; }
        [DisplayName("检验检疫证明")]
        public string InspectionCertification { get; set; }

        public bool Published { get; set; }

        [DisplayName("创建日期")]
        public DateTime? CreatedOn { get; set; }
        [DisplayName("最近修改日期")]
        public DateTime? UpdatedOn { get; set; }

        public string CategoryName { get; set; }

        [DisplayName("目录名称:")]
        public string CategoryId { get; set; }

        //categories
        public IList<SelectListItem> AvailableCategories { get; set; }
    }
}