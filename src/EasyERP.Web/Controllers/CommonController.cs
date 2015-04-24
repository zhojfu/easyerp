namespace EasyERP.Web.Controllers
{
    using EasyErp.Core;
    using EasyERP.Web.Models.Common;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.Mvc;

    public class CommonController : BasePublicController
    {
        private readonly IWorkContext workContext;

        public CommonController(
            IWorkContext workContext
            )
        {
            this.workContext = workContext;
        }

        //page not found
        public ActionResult PageNotFound()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;

            return View();
        }

        //footer
        [ChildActionOnly]
        public ActionResult JavaScriptDisabledWarning()
        {
            return PartialView();
        }

        //header links
        [ChildActionOnly]
        public ActionResult HeaderLinks()
        {
            var user = workContext.CurrentUser;

            var model = new HeaderLinksModel
            {
                Username = user.Name
            };

            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult AdminHeaderLinks()
        {
            var user = workContext.CurrentUser;

            var model = new AdminHeaderLinksModel();

            return PartialView(model);
        }

        //footer
        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = new FooterModel();

            return PartialView(model);
        }

        //favicon
        [ChildActionOnly]
        public ActionResult Favicon()
        {
            var model = new FaviconModel();
            return PartialView(model);
        }

        public ActionResult RobotsTextFile()
        {
            var disallowPaths = new List<string>
            {
                "/",
                "/bin/"
            };

            var sb = new StringBuilder();
            sb.Append("User-agent: *");
            sb.Append(Environment.NewLine);

            //usual paths
            foreach (var path in disallowPaths)
            {
                sb.AppendFormat("Disallow: {0}", path);
                sb.Append(Environment.NewLine);
            }

            Response.ContentType = "text/plain";
            Response.Write(sb.ToString());
            return null;
        }

        public ActionResult GenericUrl()
        {
            //seems that no entity was found
            return InvokeHttp404();
        }

        //store is closed
        public ActionResult StoreClosed()
        {
            return View();
        }
    }
}