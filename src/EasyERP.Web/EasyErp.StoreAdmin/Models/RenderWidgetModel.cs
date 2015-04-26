namespace EasyErp.StoreAdmin.Models
{
    using Infrastructure.Domain.Model;
    using System.Web.Routing;

    public class RenderWidgetModel : BaseEntity
    {
        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public RouteValueDictionary RouteValues { get; set; }
    }
}