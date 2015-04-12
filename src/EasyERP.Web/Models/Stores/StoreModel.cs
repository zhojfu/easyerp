namespace EasyERP.Web.Models.Stores
{
    using EasyERP.Web.Validators.Stores;
    using FluentValidation.Attributes;
    using Infrastructure.Domain.Model;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    [Validator(typeof(StoreValidator))]
    public partial class StoreModel : BaseEntity
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