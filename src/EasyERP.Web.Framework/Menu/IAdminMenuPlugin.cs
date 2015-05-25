namespace EasyERP.Web.Framework.Menu
{
    public interface IAdminMenuPlugin
    {
        bool Authenticate();
        SiteMapNode BuildMenuItem();
    }
}