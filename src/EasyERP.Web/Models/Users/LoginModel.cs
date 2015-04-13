namespace EasyERP.Web.Models.Users
{
    using FluentValidation.Attributes;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class LoginModel
    {
        public bool CheckoutAsGuest { get; set; }

        [AllowHtml]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [AllowHtml]
        public string Password { get; set; }

        public bool DisplayCaptcha { get; set; }
    }
}