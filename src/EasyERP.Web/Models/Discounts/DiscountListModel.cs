namespace EasyERP.Web.Models.Discounts
{
    using Infrastructure.Domain.Model;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public partial class DiscountListModel : BaseEntity
    {
        public DiscountListModel()
        {
            this.AvailableDiscountTypes = new List<SelectListItem>();
        }

        [AllowHtml]
        public string SearchDiscountCouponCode { get; set; }

        public int SearchDiscountTypeId { get; set; }

        public IList<SelectListItem> AvailableDiscountTypes { get; set; }
    }
}