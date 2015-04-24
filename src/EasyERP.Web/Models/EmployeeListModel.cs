namespace EasyERP.Web.Models
{
    using EasyERP.Web.Framework.Mvc;

    public class EmployeeListModel : BaseModel
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Sex { get; set; }

        public string CellPhone { get; set; }

        public string NativePlace { get; set; }

        public string IdNumber { get; set; }

        public string Address { get; set; }

        public string Department { get; set; }

        public string Education { get; set; }
    }
}