namespace EasyERP.Web.Models.StoreSale
{
    public class OrderListModel
    {
        public int Id { get; set; }

        public string CreatedOn { get; set; }

        public string Title { get; set; }

        public double TotalPrice { get; set; }

        public string Owner { get; set; }

        public string Address { get; set; }
    }
}