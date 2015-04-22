namespace EasyERP.Web.Models.Discounts
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Infrastructure.Domain.Model;

    public class DiscountListModel : BaseEntity
    {
        public DiscountListModel()
        {
            AvailableDiscountTypes = new List<SelectListItem>();
        }

        [AllowHtml]
        public string SearchDiscountCouponCode { get; set; }

        public int SearchDiscountTypeId { get; set; }
        public IList<SelectListItem> AvailableDiscountTypes { get; set; }
    }
}