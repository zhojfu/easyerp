namespace EasyERP.Web.Models.Users
{
    using EasyERP.Web.Framework.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class LoginModel : BaseModel
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