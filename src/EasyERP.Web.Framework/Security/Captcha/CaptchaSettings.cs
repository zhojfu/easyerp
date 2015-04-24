namespace EasyERP.Web.Framework.Security.Captcha
{
    using EasyErp.Core.Configuration.Settings;

    public class CaptchaSettings : ISettings
    {
        public bool Enabled { get; set; }
        public string ReCaptchaPublicKey { get; set; }
        public string ReCaptchaPrivateKey { get; set; }
        public string ReCaptchaTheme { get; set; }
    }
}