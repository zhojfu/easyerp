namespace EasyERP.Web.Models
{
    using EasyERP.Web.Framework.Mvc;

    public class WorkTime : BaseModel
    {
        public string employeeId { get; set; }

        public string employeeName { get; set; }
    }
}