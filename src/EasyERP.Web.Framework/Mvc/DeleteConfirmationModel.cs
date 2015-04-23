namespace EasyERP.Web.Framework.Mvc
{
    using Infrastructure.Domain.Model;

    public class DeleteConfirmationModel : BaseEntityModel
    {
        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public string WindowId { get; set; }
    }
}