using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyERP.Web.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class EmployeeModel
    {
        [DisplayName("名")]
        [StringLength(2)]
        public string FirstName { get; set; }
        [DisplayName("姓")]
        public string LastName { get; set; }
        [DisplayName("身份证号")]
        public string IdNumber { get; set; }
        [DisplayName("性别")]
        public bool Male { get; set; }
        [DisplayName("出生年月")]
        public DateTime Birth { get; set; }
        [DisplayName("民族")]
        public string Race { get; set; }
        [DisplayName("婚否")]
        public bool Married { get; set; }
        [DisplayName("籍贯")]
        public string NativePlace { get; set; }
        [DisplayName("现住地址")]
        public string Address { get; set; }
        [DisplayName("家庭电话")]
        public string HomePhone { get; set; }
        [DisplayName("手机")]
        public string CellPhone { get; set; }
        [DisplayName("学历")]
        public string EduBackground { get; set; }
        [DisplayName("邮箱")]
        public string EMail { get; set; }
        [DisplayName("照片")]
        public string Photo { get; set; }
        [DisplayName("部门")]
        public long Department { get; set; }
        [DisplayName("月薪")]
        public double SalaryOfMonth { get; set; }

    }
}