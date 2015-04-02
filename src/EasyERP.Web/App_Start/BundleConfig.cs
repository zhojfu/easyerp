using System.Web.Optimization;

namespace EasyERP.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/bundles/kendo/css").Include(
                       "~/Content/kendo/2014.2.716/kendo.default.min.css",
                       "~/Content/kendo/2014.2.716/kendo.common.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/kendo/js").Include(
                        "~/Scripts/kendo/2014.2.716/kendo.all.min.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/site.css"));
        }
    }
}