namespace EasyERP.Web.Framework.Security
{
    using System;
    using System.Web.Mvc;
    using EasyErp.Core;
    using EasyErp.Core.Infrastructure;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HttpsRequirementAttribute : FilterAttribute, IAuthorizationFilter
    {
        public HttpsRequirementAttribute(SslRequirement sslRequirement)
        {
            SslRequirement = sslRequirement;
        }

        public SslRequirement SslRequirement { get; set; }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            //don't apply filter to child methods
            if (filterContext.IsChildAction)
            {
                return;
            }

            // only redirect for GET requests,
            // otherwise the browser might not propagate the verb and request body correctly.
            if (!string.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            switch (SslRequirement)
            {
                case SslRequirement.Yes:
                {
                }
                    break;

                case SslRequirement.No:
                {
                    var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                    var currentConnectionSecured = webHelper.IsCurrentConnectionSecured();
                    if (currentConnectionSecured)
                    {
                        //redirect to HTTP version of page
                        //string url = "http://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;
                        var url = webHelper.GetThisPageUrl(true, false);

                        //301 (permanent) redirection
                        filterContext.Result = new RedirectResult(url, true);
                    }
                }
                    break;

                case SslRequirement.NoMatter:
                {
                    //do nothing
                }
                    break;

                default:
                    throw new Exception("Not supported SslProtected parameter");
            }
        }
    }
}