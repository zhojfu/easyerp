namespace EasyERP.Web.Controllers
{
    using Doamin.Service.Products;
    using Doamin.Service.Security;
    using Doamin.Service.Vendors;
    using EasyErp.Core;
    using EasyErp.Core.Configuration.Settings;
    using EasyERP.Web.Framework.Security;
    using EasyERP.Web.Models.Common;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Web.Mvc;

    public partial class CommonController : BasePublicController
    {
        #region Constructors

        private readonly CommonSettings _commonSettings;

        private readonly IWorkContext _workContext;

        public CommonController(
            CommonSettings commonSettings,
            IWorkContext workContext
            )
        {
            this._commonSettings = commonSettings;
            this._workContext = workContext;
        }

        #endregion Constructors

        #region Methods

        //page not found
        public ActionResult PageNotFound()
        {
            this.Response.StatusCode = 404;
            this.Response.TrySkipIisCustomErrors = true;

            return this.View();
        }

        //footer
        [ChildActionOnly]
        public ActionResult JavaScriptDisabledWarning()
        {
            if (!this._commonSettings.DisplayJavaScriptDisabledWarning)
                return this.Content("");

            return this.PartialView();
        }

        //header links
        [ChildActionOnly]
        public ActionResult HeaderLinks()
        {
            var user = this._workContext.CurrentUser;

            var model = new HeaderLinksModel
            {
                Username = user.Name
            };

            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult AdminHeaderLinks()
        {
            var user = this._workContext.CurrentUser;

            var model = new AdminHeaderLinksModel
            {
            };

            return PartialView(model);
        }

        //footer
        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = new FooterModel
            {
            };

            return PartialView(model);
        }

        //favicon
        [ChildActionOnly]
        public ActionResult Favicon()
        {
            var model = new FaviconModel
            {
            };
            return PartialView(model);
        }

        public ActionResult RobotsTextFile()
        {
            var disallowPaths = new List<string>
                                    {
                                        "/bin/",
                                        "/content/files/",
                                        "/content/files/exportimport/",
                                        "/country/getstatesbycountryid",
                                        "/install",
                                        "/setproductreviewhelpfulness",
                                    };
            var localizableDisallowPaths = new List<string>
                                               {
                                                   "/addproducttocart/catalog/",
                                                   "/addproducttocart/details/",
                                                   "/backinstocksubscriptions/manage",
                                                   "/boards/forumsubscriptions",
                                                   "/boards/forumwatch",
                                                   "/boards/postedit",
                                                   "/boards/postdelete",
                                                   "/boards/postcreate",
                                                   "/boards/topicedit",
                                                   "/boards/topicdelete",
                                                   "/boards/topiccreate",
                                                   "/boards/topicmove",
                                                   "/boards/topicwatch",
                                                   "/cart",
                                                   "/checkout",
                                                   "/checkout/billingaddress",
                                                   "/checkout/completed",
                                                   "/checkout/confirm",
                                                   "/checkout/shippingaddress",
                                                   "/checkout/shippingmethod",
                                                   "/checkout/paymentinfo",
                                                   "/checkout/paymentmethod",
                                                   "/clearcomparelist",
                                                   "/compareproducts",
                                                   "/customer/avatar",
                                                   "/customer/activation",
                                                   "/customer/addresses",
                                                   "/customer/changepassword",
                                                   "/customer/checkusernameavailability",
                                                   "/customer/downloadableproducts",
                                                   "/customer/info",
                                                   "/deletepm",
                                                   "/emailwishlist",
                                                   "/inboxupdate",
                                                   "/newsletter/subscriptionactivation",
                                                   "/onepagecheckout",
                                                   "/order/history",
                                                   "/orderdetails",
                                                   "/passwordrecovery/confirm",
                                                   "/poll/vote",
                                                   "/privatemessages",
                                                   "/returnrequest",
                                                   "/returnrequest/history",
                                                   "/rewardpoints/history",
                                                   "/sendpm",
                                                   "/sentupdate",
                                                   "/shoppingcart/*",
                                                   "/subscribenewsletter",
                                                   "/topic/authenticate",
                                                   "/viewpm",
                                                   "/uploadfileproductattribute",
                                                   "/uploadfilecheckoutattribute",
                                                   "/wishlist",
                                               };

            const string newLine = "\r\n"; //Environment.NewLine
            var sb = new StringBuilder();
            sb.Append("User-agent: *");
            sb.Append(newLine);

            //usual paths
            foreach (var path in disallowPaths)
            {
                sb.AppendFormat("Disallow: {0}", path);
                sb.Append(newLine);
            }
            //localizable paths (without SEO code)
            foreach (var path in localizableDisallowPaths)
            {
                sb.AppendFormat("Disallow: {0}", path);
                sb.Append(newLine);
            }

            this.Response.ContentType = "text/plain";
            this.Response.Write(sb.ToString());
            return null;
        }

        public ActionResult GenericUrl()
        {
            //seems that no entity was found
            return this.InvokeHttp404();
        }

        //store is closed
        public ActionResult StoreClosed()
        {
            return this.View();
        }

        #endregion Methods
    }
}