namespace EasyERP.Web.Controllers
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using EasyERP.Web.Framework.Controllers;
    using EasyERP.Web.Framework.Security;

    [HttpsRequirement(SslRequirement.Yes)]
    [AdminAuthorize]
    [AdminAntiForgery]
    public abstract class BaseAdminController : BaseController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            //set work context to admin mode
            //EngineContext.Current.Resolve<IWorkContext>().IsAdmin = true;

            base.Initialize(requestContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                LogException(filterContext.Exception);
            }
            base.OnException(filterContext);
        }

        protected void SaveSelectedTabIndex(int? index = null, bool persistForTheNextRequest = true)
        {
            if (!index.HasValue)
            {
                int tmp;
                if (int.TryParse(Request.Form["selected-tab-index"], out tmp))
                {
                    index = tmp;
                }
            }
            if (index.HasValue)
            {
                var dataKey = "nop.selected-tab-index";
                if (persistForTheNextRequest)
                {
                    TempData[dataKey] = index;
                }
                else
                {
                    ViewData[dataKey] = index;
                }
            }
        }
    }
}