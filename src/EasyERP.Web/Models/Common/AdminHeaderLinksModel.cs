namespace EasyERP.Web.Models.Common
{
    public partial class AdminHeaderLinksModel
    {
        public string ImpersonatedCustomerEmailUsername { get; set; }

        public bool IsCustomerImpersonated { get; set; }

        public bool DisplayAdminLink { get; set; }
    }
}