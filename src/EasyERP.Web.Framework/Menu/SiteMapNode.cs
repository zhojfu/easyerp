//code from Telerik MVC Extensions

namespace EasyERP.Web.Framework.Menu
{
    using System.Collections.Generic;
    using System.Web.Routing;

    public class SiteMapNode
    {
        public SiteMapNode()
        {
            RouteValues = new RouteValueDictionary();
            ChildNodes = new List<SiteMapNode>();
        }

        public string Title { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public RouteValueDictionary RouteValues { get; set; }

        public string Url { get; set; }

        public IList<SiteMapNode> ChildNodes { get; set; }

        public string ImageUrl { get; set; }

        public bool Visible { get; set; }
    }
}