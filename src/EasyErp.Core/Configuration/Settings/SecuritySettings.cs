namespace EasyErp.Core.Configuration.Settings
{
    using System.Collections.Generic;

    public class SecuritySettings : ISettings
    {
        public bool ForceSslForAllPages { get; set; }

        public string EncryptionKey { get; set; }

        public bool EnableXsrfProtectionForAdminArea { get; set; }

        public bool EnableXsrfProtectionForPublicStore { get; set; }
    }
}