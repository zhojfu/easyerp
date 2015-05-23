namespace EasyERP.Web.Models.Stores
{
    using System;
    using System.ComponentModel;
    using System.Web.Mvc;
    using EasyERP.Web.Framework.Mvc;
    using EasyERP.Web.Validators.Stores;
    using FluentValidation.Attributes;

    [Validator(typeof(StoreValidator))]
    public class StoreModel : BaseEntityModel
    {
        [AllowHtml]
        [DisplayName("店名：")]
        public string Name { get; set; }

        [AllowHtml]
        [DisplayName("描述：")]
        public string ShortDescription { get; set; }

        [AllowHtml]
        [DisplayName("详细信息：")]
        public string FullDescription { get; set; }

        public int DisplayOrder { get; set; }

        [AllowHtml]
        public string CompanyName { get; set; }

        [AllowHtml]
        [DisplayName("地址：")]
        public string StoreAddress { get; set; }

        [AllowHtml]
        public string StorePhoneNumber { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}