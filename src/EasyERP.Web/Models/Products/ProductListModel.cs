
namespace EasyERP.Web.Models.Products
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web.Mvc;
    using EasyERP.Web.Framework.Mvc;
    using EasyERP.Web.Validators.Products;
    using FluentValidation.Attributes;

    [Validator(typeof(ProductListModelValidator))]
    public class ProductListModel : BaseModel
    {
        public ProductListModel()
        {
            AvailableCategories = new List<SelectListItem>();
            AvailableStores = new List<SelectListItem>();
            AvailablePublishedOptions = new List<SelectListItem>();
        }

        [AllowHtml]
        [DisplayName("产品名:")]
        public string SearchProductName { get; set; }

        [DisplayName("目录:")]
        public int SearchCategoryId { get; set; }

        [DisplayName("门店:")]
        public int SearchStoreId { get; set; }

        public List<int> SearchStoreIds { get; set; }

        public IList<SelectListItem> AvailableCategories { get; set; }

        public IList<SelectListItem> AvailableStores { get; set; }

        public IList<SelectListItem> AvailablePublishedOptions { get; set; }
    }
}