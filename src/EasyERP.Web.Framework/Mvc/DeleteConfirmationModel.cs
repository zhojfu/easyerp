namespace EasyERP.Web.Framework.Mvc
{
    using Infrastructure.Domain.Model;

    public class DeleteConfirmationModel : BaseEntity
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string WindowId { get; set; }
    }
}