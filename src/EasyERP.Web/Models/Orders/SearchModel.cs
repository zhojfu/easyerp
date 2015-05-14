namespace EasyERP.Web.Models.Orders
{
    using EasyERP.Web.Framework.Mvc;
    using Infrastructure.Domain.Model;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class SearchModel : BaseModel
    {
        public int OrderStatus { get; set; }

        public int PayStatus { get; set; }

        public int StoreId { get; set; }
    }
}