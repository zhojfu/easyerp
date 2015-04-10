namespace EasyERP.Web.Framework.Menu
{
    public interface IAdminMenuPlugin
    {
        /// <summary>
        /// Authenticate a user (can he see this plugin menu item?)
        /// </summary>
        /// <returns></returns>
        bool Authenticate();

        /// <summary>
        /// Build menu item
        /// </summary>
        /// <returns>Site map item</returns>
        SiteMapNode BuildMenuItem();
    }
}