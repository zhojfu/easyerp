namespace EasyERP.Web.Models.Stores
{
    using System;
    using System.Web.Mvc;
    using EasyERP.Web.Validators.Stores;
    using FluentValidation.Attributes;
    using Infrastructure.Domain.Model;

    [Validator(typeof(StoreValidator))]
    public class StoreModel : BaseEntity
    {
        [AllowHtml]
        public string Name { get; set; }

        [AllowHtml]
        public string ShortDescription { get; set; }

        [AllowHtml]
        public string FullDescription { get; set; }

        public int DisplayOrder { get; set; }

        [AllowHtml]
        public string CompanyName { get; set; }

        [AllowHtml]
        public string CompanyAddress { get; set; }

        [AllowHtml]
        public string CompanyPhoneNumber { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}