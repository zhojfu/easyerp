namespace EasyERP.Web.Framework.Themes
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class ThemeableRazorViewEngine : ThemeableVirtualPathProviderViewEngine
    {
        public ThemeableRazorViewEngine()
        {
            AreaViewLocationFormats = new[]
            {
                //themes
                "~/Areas/{2}/Themes/{3}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Themes/{3}/Views/Shared/{0}.cshtml",

                //default
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };

            AreaMasterLocationFormats = new[]
            {
                //themes
                "~/Areas/{2}/Themes/{3}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Themes/{3}/Views/Shared/{0}.cshtml",

                //default
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };

            AreaPartialViewLocationFormats = new[]
            {
                //themes
                "~/Areas/{2}/Themes/{3}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Themes/{3}/Views/Shared/{0}.cshtml",

                //default
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };

            ViewLocationFormats = new[]
            {
                //themes
                "~/Themes/{2}/Views/{1}/{0}.cshtml",
                "~/Themes/{2}/Views/Shared/{0}.cshtml",

                //default
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",

                //StoreAdmin
                "~/EasyERP.StoreAdmin/Views/{1}/{0}.cshtml",
                "~/EasyERP.StoreAdmin/Views/Shared/{0}.cshtml"
            };

            MasterLocationFormats = new[]
            {
                //themes
                "~/Themes/{2}/Views/{1}/{0}.cshtml",
                "~/Themes/{2}/Views/Shared/{0}.cshtml",

                //default
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };

            PartialViewLocationFormats = new[]
            {
                //themes
                "~/Themes/{2}/Views/{1}/{0}.cshtml",
                "~/Themes/{2}/Views/Shared/{0}.cshtml",

                //default
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",

                //StoreAdmin
                "~/EasyERP.StoreAdmin/Views/{1}/{0}.cshtml",
                "~/EasyERP.StoreAdmin/Views/Shared/{0}.cshtml"
            };

            FileExtensions = new[] { "cshtml" };
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            IEnumerable<string> fileExtensions = FileExtensions;
            return new RazorView(controllerContext, partialPath, null, false, fileExtensions);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            IEnumerable<string> fileExtensions = FileExtensions;
            return new RazorView(controllerContext, viewPath, masterPath, true, fileExtensions);
        }
    }
}