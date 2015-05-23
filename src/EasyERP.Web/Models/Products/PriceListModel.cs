namespace EasyERP.Web.Models.Products
{
    using Domain.Model.Products;
    using Domain.Model.Stores;
    using EasyERP.Web.Framework.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class PriceListModel : BaseModel
    {
        public PriceListModel()
        {
            AvailableStores = new List<SelectListItem>();
            AvaliableProducts = new List<SelectListItem>();
        }

        public List<SelectListItem> AvailableStores { get; set; }

        public List<SelectListItem> AvaliableProducts { get; set; }

        public int StoreId { get; set; }

        public virtual Store Store { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}