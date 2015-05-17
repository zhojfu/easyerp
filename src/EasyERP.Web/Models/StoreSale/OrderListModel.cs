using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyERP.Web.Models.StoreSale
{
    public class OrderListModel
    {
        public string CreatedOn { get; set; }
        public string Title { get; set; }
        public double TotalPrice { get; set; }
        public string Owner { get; set; }
        public string Address { get; set; }
    }
}