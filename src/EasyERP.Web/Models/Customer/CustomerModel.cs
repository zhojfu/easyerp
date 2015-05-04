namespace EasyERP.Web.Models.Customer
{
    using EasyERP.Web.Framework.Mvc;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class CustomerModel : BaseEntityModel
    {
        [DisplayName("名称")]
        [StringLength(20, ErrorMessage = "不能超过20个字符")]
        [Required]
        public string Name{ get; set; }

        [DisplayName("身份证号")]
        [Required]
        [RegularExpression(@"^\d{18}[xX]{0,1}$")]
        public string IdNumber { get; set; }

        [DisplayName("性别")]
        public bool Male { get; set; }

        [DisplayName("出生年月")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birth { get; set; }

        [DisplayName("地址")]
        [StringLength(100)]
        public string Address { get; set; }

        [DisplayName("联系电话")]
        [StringLength(20)]
        public string TelePhone { get; set; }

        [DisplayName("备注")]
        public string Description { get; set; }
    }
}