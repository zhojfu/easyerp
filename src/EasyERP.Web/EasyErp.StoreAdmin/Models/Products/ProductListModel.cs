namespace EasyErp.StoreAdmin.Models.Products
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public partial class ProductListModel
    {
        public ProductListModel()
        {
            this.AvailableCategories = new List<SelectListItem>();
            this.AvailableManufacturers = new List<SelectListItem>();
            this.AvailableStores = new List<SelectListItem>();
            this.AvailableWarehouses = new List<SelectListItem>();
            this.AvailableVendors = new List<SelectListItem>();
            this.AvailableProductTypes = new List<SelectListItem>();
            this.AvailablePublishedOptions = new List<SelectListItem>();
        }

        [AllowHtml]
        public string SearchProductName { get; set; }

        public int SearchCategoryId { get; set; }

        public bool SearchIncludeSubCategories { get; set; }

        public int SearchManufacturerId { get; set; }

        public int SearchStoreId { get; set; }

        public int SearchVendorId { get; set; }

        public int SearchWarehouseId { get; set; }

        public int SearchProductTypeId { get; set; }

        public int SearchPublishedId { get; set; }

        [AllowHtml]
        public string GoDirectlyToSku { get; set; }

        public bool DisplayProductPictures { get; set; }

        public bool IsLoggedInAsVendor { get; set; }

        public IList<SelectListItem> AvailableCategories { get; set; }

        public IList<SelectListItem> AvailableManufacturers { get; set; }

        public IList<SelectListItem> AvailableStores { get; set; }

        public IList<SelectListItem> AvailableWarehouses { get; set; }

        public IList<SelectListItem> AvailableVendors { get; set; }

        public IList<SelectListItem> AvailableProductTypes { get; set; }

        public IList<SelectListItem> AvailablePublishedOptions { get; set; }
    }
}