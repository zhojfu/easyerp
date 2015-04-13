namespace EasyErp.StoreAdmin
{
    using System.Web.Mvc;

    public class StoreAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "StoreAdmin"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "StoreAdmin_default",
                "StoreAdmin/{controller}/{action}/{id}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    area = "Store",
                    id = UrlParameter.Optional
                },
                new[] { "EasyErp.StoreAdmin.Controllers" }
                );
        }
    }
}