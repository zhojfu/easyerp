using System.Web.Mvc;
using System.Web.Routing;

namespace EasyERP.Web
{
    using EasyErp.Core;
    using EasyErp.Core.Infrastructure;
    using EasyERP.Web.App_Start;
    using EasyERP.Web.Framework;
    using FluentValidation.Mvc;
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Web;
    using System.Web.Optimization;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //initialize engine context
            EngineContext.Initialize(false);

            AutoMapperBootstraper.RegisterModelMapper();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //fluent validation
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new ValidatorFactory()));
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //we don't do it in Application_BeginRequest because a user is not authenticated yet
            SetWorkingCulture();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();

            //log error
            LogException(exception);

            //process 404 HTTP errors
            var httpException = exception as HttpException;
            if (httpException != null && httpException.GetHttpCode() == 404)
            {
                var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                if (!webHelper.IsStaticResource(this.Request))
                {
                    Response.Clear();
                    Server.ClearError();
                    Response.TrySkipIisCustomErrors = true;

                    // Call target Controller and pass the routeData.
                    //IController errorController = EngineContext.Current.Resolve<CommonController>();

                    //var routeData = new RouteData();
                    //routeData.Values.Add("controller", "Common");
                    //routeData.Values.Add("action", "PageNotFound");

                    //errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
                }
            }
        }

        protected void SetWorkingCulture()
        {
            //ignore static resources
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            if (webHelper.IsStaticResource(this.Request))
                return;

            //keep alive page requested (we ignore it to prevent creation of guest customer records)
            string keepAliveUrl = string.Format("{0}keepalive/index", webHelper.GetStoreLocation());
            if (webHelper.GetThisPageUrl(false).StartsWith(keepAliveUrl, StringComparison.InvariantCultureIgnoreCase))
                return;

            if (webHelper.GetThisPageUrl(false).StartsWith(string.Format("{0}admin", webHelper.GetStoreLocation()),
                StringComparison.InvariantCultureIgnoreCase))
            {
                //admin area

                //always set culture to 'en-US'
                //we set culture of admin area to 'en-US' because current implementation of Telerik grid
                //doesn't work well in other cultures
                //e.g., editing decimal value in russian culture
                CommonHelper.SetTelerikCulture();
            }
            else
            {
                //public store
                var culture = CultureInfo.CreateSpecificCulture("zh-CN");
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }

        protected void LogException(Exception exc)
        {
        }
    }
}