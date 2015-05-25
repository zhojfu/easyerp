namespace EasyErp.StoreAdmin.Models
{
    using System.Web.Routing;
    using Infrastructure.Domain.Model;

    public class RenderWidgetModel : BaseEntity
    {
        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public RouteValueDictionary RouteValues { get; set; }
    }
}