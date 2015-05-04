namespace EasyERP.Web.Models.Employee
{
    using EasyERP.Web.Framework.Mvc;
    using EasyERP.Web.Validators.Employees;
    using FluentValidation.Attributes;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    //[Validator(typeof(EmployeeValidator))]
    public class EmployeeModel : BaseEntityModel
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

        [DisplayName("民族")]
        public string Race { get; set; }

        [DisplayName("婚否")]
        public bool Married { get; set; }

        [DisplayName("籍贯")]
        [StringLength(20)]
        public string NativePlace { get; set; }

        [DisplayName("现住地址")]
        [StringLength(100)]
        public string Address { get; set; }

        [DisplayName("家庭电话")]
        [StringLength(20)]
        public string HomePhone { get; set; }

        [DisplayName("手机")]
        [RegularExpression(@"^\d{11}$")]
        public string CellPhone { get; set; }

        [DisplayName("学历")]
        [StringLength(10)]
        public string Education { get; set; }

        [DisplayName("邮箱")]
        [StringLength(50)]
        public string EMail { get; set; }

        [DisplayName("照片")]
        public string Photo { get; set; }

        [DisplayName("部门")]
        [StringLength(50)]
        [Required]
        public string Department { get; set; }

        [DisplayName("月薪")]
        [RegularExpression(@"^\d+.{0,1}\d*$", ErrorMessage = "必须为数字")]
        public double SalaryOfMonth { get; set; }
    }
}