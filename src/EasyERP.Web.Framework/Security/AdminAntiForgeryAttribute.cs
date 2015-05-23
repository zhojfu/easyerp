namespace EasyERP.Web.Framework.Security
{
    using System;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminAntiForgeryAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly bool _ignore;

        public AdminAntiForgeryAttribute(bool ignore = false)
        {
            _ignore = ignore;
        }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (_ignore)
            {
                return;
            }

            //don't apply filter to child methods
            if (filterContext.IsChildAction)
            {
                return;
            }

            //only POST requests
            if (!string.Equals(filterContext.HttpContext.Request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            //var securitySettings = EngineContext.Current.Resolve<SecuritySettings>();
            //if (!securitySettings.EnableXsrfProtectionForAdminArea)
            return;

            var validator = new ValidateAntiForgeryTokenAttribute();
            validator.OnAuthorization(filterContext);
        }
    }
}