namespace EasyERP.Web.Models.Stores
{
    using EasyERP.Web.Validators.Stores;
    using FluentValidation.Attributes;
    using Infrastructure.Domain.Model;
    using System.Collections.Generic;
    using System.Web.Mvc;

    [Validator(typeof(StoreValidator))]
    public partial class StoreModel : BaseEntity
    {
        [AllowHtml]
        public string Name { get; set; }

        [AllowHtml]
        public string Url { get; set; }

        public virtual bool SslEnabled { get; set; }

        [AllowHtml]
        public virtual string SecureUrl { get; set; }

        [AllowHtml]
        public string Hosts { get; set; }

        public int DisplayOrder { get; set; }

        [AllowHtml]
        public string CompanyName { get; set; }

        [AllowHtml]
        public string CompanyAddress { get; set; }

        [AllowHtml]
        public string CompanyPhoneNumber { get; set; }

        [AllowHtml]
        public string CompanyVat { get; set; }
    }
}