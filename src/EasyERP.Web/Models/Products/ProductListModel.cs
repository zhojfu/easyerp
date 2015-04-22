namespace EasyERP.Web.Models.Products
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class ProductListModel
    {
        public ProductListModel()
        {
            AvailableCategories = new List<SelectListItem>();
            AvailableStores = new List<SelectListItem>();
            AvailablePublishedOptions = new List<SelectListItem>();
        }

        [AllowHtml]
        public string SearchProductName { get; set; }

        public int SearchCategoryId { get; set; }
        public int SearchStoreId { get; set; }
        public List<int> SearchStoreIds { get; set; }
        public int SearchPublishedId { get; set; }

        [AllowHtml]
        public string GoDirectlyToSku { get; set; }

        public IList<SelectListItem> AvailableCategories { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; }
        public IList<SelectListItem> AvailablePublishedOptions { get; set; }
    }
}