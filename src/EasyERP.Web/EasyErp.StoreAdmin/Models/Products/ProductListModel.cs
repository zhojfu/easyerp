namespace EasyErp.StoreAdmin.Models.Products
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class ProductListModel
    {
        public ProductListModel()
        {
            AvailableCategories = new List<SelectListItem>();
        }

        [AllowHtml]
        public string SearchProductName { get; set; }

        public int SearchCategoryId { get; set; }

        public IList<SelectListItem> AvailableCategories { get; set; }
    }
}